﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCardEvent : MonoBehaviour
{
    [SerializeField] ShowcaseCard cardShowcase;

    Card card;

    void Start()
    {
        Card[] addableCards = FindObjectOfType<CardHolders>().GetAllPlayerCards();
        card = addableCards[Random.Range(0, addableCards.Length)];
        cardShowcase.PutInformation(card, GetBuildingLevel());
    }

    public void Activated()
    {
        FindObjectOfType<CardHolders>().RemovePlayerCard(card);
    }

    int GetBuildingLevel()
    {
        int buildingLevel = -1;
        Card[] playerCards = FindObjectOfType<CardHolders>().GetAllPlayerCards();
        foreach (Card lookingCard in playerCards)
        {
            if (card.GetPrefabs().GetBuilding(0).GetID() == lookingCard.GetPrefabs().GetBuilding(0).GetID())
            {
                buildingLevel++;
            }
        }
        return buildingLevel;
    }
}