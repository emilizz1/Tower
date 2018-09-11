﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsShowcase : MonoBehaviour
{
    [SerializeField] CardHolder playerCards;
    [SerializeField] ShowcaseCard[] cards;
    [SerializeField] GameObject[] Pages;

    List<Buildings> buildings = new List<Buildings>();

    int currentlyActiveCards = 0;

    void Start()
    {
        ShowcaseCards();
        TurnPageTo(0);
    }

    void ShowcaseCards()
    {
        foreach (Card card in playerCards.GetAllCards())
        {
            SetupCard(card);
        }
    }

    private void SetupCard(Card card)
    {
        
        cards[currentlyActiveCards].PutInformation(card, GetBuildingLevel(card)); //todo count building Level
        currentlyActiveCards++;
    }

    void TurnOffCards()
    {
        foreach (ShowcaseCard card in cards)
        {
            card.gameObject.SetActive(false);
        }
        currentlyActiveCards = 0;
    }

    int GetBuildingLevel(Card card)
    {
        int buildingLevel = 0;
        Buildings currentlyLooking = card.GetPrefabs().GetBuilding(0);
        foreach(Buildings building in buildings)
        {
            if(building == currentlyLooking)
            {
                buildingLevel++;
            }
        }
        buildings.Add(currentlyLooking);
        return buildingLevel;
    }

    public void TurnPageTo(int pageNumber)
    {
        foreach(GameObject page in Pages)
        {
            page.SetActive(false);
        }
        Pages[pageNumber].SetActive(true);
    }
}