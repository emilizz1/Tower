using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolders : MonoBehaviour
{
    [SerializeField] CardHolder playerCards;
    [SerializeField] CardHolder startignPlayerCards;
    [SerializeField] CardHolder addableCards;
    [SerializeField] CardHolder startingAddableCards;

    public static CardHolders control;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != null)
        {
            Destroy(gameObject);
        }
        if (playerCards.GetAllCards().Length == 0)
        {
            RemoveAllCards(playerCards);
            AddAllCards(startignPlayerCards, playerCards);
            RemoveAllCards(addableCards);
            AddAllCards(startingAddableCards, addableCards);
        }
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
