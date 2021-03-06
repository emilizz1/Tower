﻿using System.Collections.Generic;
using Towers.BuildingsN;
using Towers.Resources;
using UnityEngine;

namespace Towers.CardN
{
    public class CardManager : MonoBehaviour
    {
        Cards[] cards;
        List<GameObject> enemies = new List<GameObject>();
        ResourcesManager rM;
        BuildingsHolder prefabs;
        BuildingPlacementManager bPM;
        Deck deck;
        Discard discard;
        Card[] selectedCards;
        Animator animator;
        
        int cardSelected;
        bool firstRound = true;
        bool animationIdle = true;

        public void Setup()
        {
            cards = GetComponentsInChildren<Cards>();
            bPM = FindObjectOfType<BuildingPlacementManager>();
            rM = FindObjectOfType<ResourcesManager>();
            deck = FindObjectOfType<Deck>();
            discard = FindObjectOfType<Discard>();
            animator = GetComponent<Animator>();
        }

        public BuildingsHolder GetPrefabs()
        {
            return prefabs;
        }

        public List<GameObject> GetEnemiesToCome()
        {
            return enemies;
        }

        public void CardSelected(int selectedCard, bool firstChoice)
        {
            cardSelected = selectedCard;
            prefabs = cards[cardSelected].GetPrefabs();
            if (firstChoice)
            {
                if (selectedCard == 1) // Discards not selected card
                {
                    discard.DiscardCards(selectedCards[0]);
                    animator.SetTrigger("DiscardLeftCard");
                }
                else
                {
                    discard.DiscardCards(selectedCards[1]);
                    animator.SetTrigger("DiscardRightCard");
                }
                animationIdle = false;
                bPM.BuildingSelected();
            }
            else
            {
                deck.RemoveACard(cards[cardSelected].GetCard());
                SetEnemies();
                GiveGold();
                TurnCards(false);
                FindObjectOfType<LevelEnemyCard>().TurnOffEnemyCards();
            }
        }

        void SetEnemies()
        {
            enemies = new List<GameObject>();
            for (int o = 0; o < cards[cardSelected].GetEnemyAmount(); o++)
            {
                enemies.Add(cards[cardSelected].GetEnemyPrefab());
            }
        }

        void GiveGold()
        {
            if (!firstRound)
            {
                rM.AddResources(cards[cardSelected].GetEnemyResourses(), cards[cardSelected].transform);
            }
            firstRound = false;
        }

        public void SetNewCards(bool secondChoice)
        {
            int i = 0;
            if (secondChoice)
            {
                selectedCards = deck.GetNewCards().ToArray();
                animator.SetTrigger("DrawCards");
            }
            foreach (Card card in selectedCards)
            {
                cards[i++].SetupCards(secondChoice, !secondChoice, false, card);
            }
        }

        public void TurnCards(bool isItOn)
        {
            int i = 0;
            if (!isItOn)
            {
                foreach (Cards card in cards)
                {
                    card.SetupCards(false, false, false);
                }
            }
            else
            {
                foreach (Card card in selectedCards)
                {
                    cards[i++].SetupCards( true, false, false, card);
                }
            }
        }
        
        public void AnimationIdle()
        {
            animationIdle = true;
        }

        public void AnimationNotIdle()
        {
            animationIdle = false;
        }

        public bool GetAnimationIdle()
        {
            return animationIdle;
        }
    }
}