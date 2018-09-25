using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = ("Tower defense/Card Holder"))]
public class CardHolder : ScriptableObject {

    [SerializeField] Card[] cards;

    public Card[] GetAllCards()
    {
        return cards;
    }

    public void AddCard(Card cardToAdd)
    {
        Card[] temp = new Card[cards.Length + 1];
        for (int i = 0; i < cards.Length; i++)
        {
            temp[i] = cards[i];
        }
        temp[cards.Length] = cardToAdd;
        cards = temp;
    }
}
