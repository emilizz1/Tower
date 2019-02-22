using System.Collections.Generic;
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
        LevelEnemyCard levelEnemyCard;
        
        int cardSelected;
        bool firstRound = true;

        public void Setup()
        {
            cards = GetComponentsInChildren<Cards>();
            bPM = FindObjectOfType<BuildingPlacementManager>();
            rM = FindObjectOfType<ResourcesManager>();
            deck = FindObjectOfType<Deck>();
            discard = FindObjectOfType<Discard>();
            levelEnemyCard = FindObjectOfType<LevelEnemyCard>();
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
                }
                else
                {
                    discard.DiscardCards(selectedCards[1]);
                }
                bPM.BuildingSelected();
            }
            else
            {
                deck.RemoveACard(cards[cardSelected].GetCard());
                SetEnemies();
                GiveGold();
                TurnCards(false);
                levelEnemyCard.TurnOffEnemyCards();
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
            }
            else
            {
                levelEnemyCard.PutEnemiesOnScreen();
            }
            foreach (Card card in selectedCards)
            {
                cards[i++].SetCard(card, secondChoice);
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
                    cards[i++].SetCard(card, true);
                }
            }
        }
    }
}