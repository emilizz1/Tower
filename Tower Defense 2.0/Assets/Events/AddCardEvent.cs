using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardEvent : MonoBehaviour
{
    [SerializeField] ShowcaseCard cardShowcase;

    Card card;

    void Start()
    {
        Card[] addableCards = FindObjectOfType<CardHolders>().GetAllAddableCards();
        card = addableCards[Random.Range(0, addableCards.Length)];
        cardShowcase.PutInformation(card, GetBuildingLevel());
    }

    public void Activated()
    {
        FindObjectOfType<CardHolders>().AddPlayerCard(card);
    }

    int GetBuildingLevel()
    {
        int buildingLevel = 0;
        Card[] playerCards = FindObjectOfType<CardHolders>().GetAllPlayerCards();
        foreach(Card lookingCard in playerCards)
        {
            if(card.GetPrefabs().GetBuilding(0).GetID() == lookingCard.GetPrefabs().GetBuilding(0).GetID())
            {
                buildingLevel++;
            }
        }
        return buildingLevel;
    }
}
