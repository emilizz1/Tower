using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers.CardN;
using Towers.Resources;
using UnityEngine.UI;

public class ResourceSetter : MonoBehaviour
{
    [SerializeField] GameObject[] ResourceSlots;

    void Start()
    {
        var myCards = GatherAllCards();
        var differentResources = GatherDifferentResources(myCards);
        int activeResourceSlotAmount = 0;
        DisplayResources(differentResources, ref activeResourceSlotAmount);
        FindObjectOfType<ResourcesManager>().GiveActiveResourceSlots(GetActiveResourceSlots(activeResourceSlotAmount));
    }

    GameObject[] GetActiveResourceSlots(int i)
    {
        GameObject[] activeResourceSlots = new GameObject[i];
        for (int o = 0; o < i; o++)
        {
            activeResourceSlots[o] = ResourceSlots[o];
        }
        return activeResourceSlots;
    }

    void DisplayResources(List<Resource> resources, ref int i)
    {
        foreach(Resource resource in resources)
        {
            ResourceSlots[i].GetComponentInChildren<Image>().sprite = resource.GetSprite();
            ResourceSlots[i].GetComponent<Text>().text = FindObjectOfType<ResourceHolder>().getCurrentResources(resource).ToString(); ;
            i++;
        }
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
