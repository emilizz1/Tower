﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolders : MonoBehaviour
{
    [SerializeField] CardHolder playerCards;
    [SerializeField] CardHolder startignPlayerCards;
    [SerializeField] CardHolder addableCards;
    [SerializeField] CardHolder startingAddableCards;

    void Awake()
    {
        RemoveAllCards(playerCards);
        RemoveAllCards(addableCards);
        AddAllCards(startignPlayerCards, playerCards);
        AddAllCards(startingAddableCards, addableCards);
    }

    public Card[] GetAllPlayerCards()
    {
        return playerCards.GetAllCards();
    }

    public Card[] GetAllAddableCards()
    {
        return addableCards.GetAllCards();
    }

    public void AddPlayerCard(Card cardToAdd)
    {
        playerCards.AddCard(cardToAdd);
    }

    public void AddAddableCard(Card cardToAdd)
    {
        addableCards.AddCard(cardToAdd);
    }

    public void RemovePlayerCard(Card cardToRemove)
    {
        playerCards.RemoveCard(cardToRemove);
    }

    public void RemoveAddableCard(Card cardToRemove)
    {
        addableCards.RemoveCard(cardToRemove);
    }

    void AddAllCards(CardHolder from, CardHolder to)
    {
        foreach(Card card in from.GetAllCards())
        {
            to.AddCard(card);
        }
    }

    void RemoveAllCards(CardHolder from)
    {
        foreach(Card card in from.GetAllCards())
        {
            from.RemoveCard(card);
        }
    }
}