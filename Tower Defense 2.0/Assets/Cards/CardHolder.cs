using System;
using UnityEngine;

namespace Towers.Cards
{
    [Serializable]
    [CreateAssetMenu(menuName = ("Tower defense/Card Holder"))]
    public class CardHolder : ScriptableObject
    {

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

        public void RemoveCard(Card cardToRemove)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == cardToRemove)
                {
                    cards[i] = cards[cards.Length - 1];
                    Array.Resize<Card>(ref cards, cards.Length - 1);
                }

            }
        }
    }
}