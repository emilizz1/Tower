using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsShowcase : MonoBehaviour
{
    [SerializeField] CardHolder playerCards;
    [SerializeField] ShowcaseCard[] cards;

    int currentlyActiveCards = 0;

    void ShowcaseCards()
    {
        foreach(Card card in playerCards.GetAllCards())
        {
            SetupCard(card);
        }
    }

    private void SetupCard(Card card)
    {
        cards[currentlyActiveCards].gameObject.SetActive(true);
        cards[currentlyActiveCards].PutInformation(card); //todo count building Level
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
}
