using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CardManager : MonoBehaviour
{
    Cards[] cards;
    List<GameObject> enemies = new List<GameObject>();
    ResourcesManager rM;
    BuildingsHolder prefabs;
    BuildingPlacementManager bPM;
    Deck deck;
    List<Card> selectedCards;
    LevelEnemyCard levelEnemyCard;

    int cardToRemove;
    int cardSelected;
    bool firstRound = true;
    
    void Start()
    {
        bPM = FindObjectOfType<BuildingPlacementManager>();
        rM = FindObjectOfType<ResourcesManager>();
        cards = GetComponentsInChildren<Cards>();
        deck = FindObjectOfType<Deck>();
        levelEnemyCard = FindObjectOfType<LevelEnemyCard>();
        SetNewCards(true);
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
            SetNewCards(!firstChoice);
            bPM.BuildingSelected();
        }
        else 
        {
            SetEnemies();
            GiveGold();
            SetNewCards(!firstChoice);
            TurnCards(false);
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
            rM.AddGold(cards[cardSelected].GetEnemyCardCost(), cards[cardSelected].transform);
        }
        firstRound = false;
    }

    void SetNewCards(bool secondChoice)
    {

        int i = 0;
        if (secondChoice)
        {
            deck.RemoveACard(cards[cardToRemove].GetCard());
            selectedCards = deck.GetNewCards();
            levelEnemyCard.TurnOffEnemyCards();
        }
        else
        {
            cardToRemove = cardSelected;
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
        if(!isItOn)
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
