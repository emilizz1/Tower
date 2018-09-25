using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolders : MonoBehaviour
{
    [SerializeField] CardHolder playerCards;
    [SerializeField] CardHolder addableCards;

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
        Card[] cards = playerCards.GetAllCards();
        Card[] temp = new Card[cards.Length + 1];
        for (int i = 0; i < cards.Length; i++)
        {
            temp[i] = cards[i];
        }
        temp[cards.Length] = cardToAdd;
        cards = temp;
    }

    public void AddAddableCard(Card cardToAdd)
    {
        Card[] cards = addableCards.GetAllCards();
        Card[] temp = new Card[cards.Length + 1];
        for (int i = 0; i < cards.Length; i++)
        {
            temp[i] = cards[i];
        }
        temp[cards.Length] = cardToAdd;
        cards = temp;
    }
}
