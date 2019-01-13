using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers.CardN;
using Towers.Resources;

public class ResourceSeter : MonoBehaviour
{
    [SerializeField] GameObject[] ResourceSlots;

    void Start()
    {
        SetupCards();
    }

    void SetupCards()
    {
        var myCards = GatherAllCards();
        
    }

    List<Card> GatherAllCards()
    {
        List<Card> gatheredCards = new List<Card>();
        foreach(Card card in FindObjectOfType<Deck>().GetLevelCards().GetAllCards())
        {
            gatheredCards.Add(card);
        }
        foreach (Card card in FindObjectOfType<CardHolders>().GetAllPlayerCards())
        {
            gatheredCards.Add(card);
        }
        return gatheredCards;
    }

    List<Resource> GatherDifferentResources(List<Card> myCards)
    {
        List<Resource> differentResources = new List<Resource>();
        foreach(Card card in myCards)
        {
            foreach(Resource resource in card.GetPrefabs().GetBuilding(0).GetBuildingUnitCost())
            {
                if (!differentResources.Contains(resource))
                {
                    differentResources.Add(resource);
                }
            }
        }
        return differentResources;
    }
}
