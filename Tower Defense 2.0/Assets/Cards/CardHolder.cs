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

    public void SetAllCars(Card[] card)
    {
        cards = card;
    }
}
