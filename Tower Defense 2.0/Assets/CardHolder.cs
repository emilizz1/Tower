using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Tower defense/Card Holder"))]
public class CardHolder : ScriptableObject {

    [SerializeField] Card[] cards;
	
	public Card GetRandomCard()
    {
        return cards[Random.Range(0, cards.Length)];
    }
}
