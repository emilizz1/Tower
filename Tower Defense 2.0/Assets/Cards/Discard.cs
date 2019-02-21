using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Towers.CardN
{
    public class Discard : MonoBehaviour
    {
        List<Card> discard = new List<Card>();
        Text text;

        void Start()
        {
            text = GetComponentInChildren<Text>();
            UpdateText();
        }

        public List<Card> GetDiscard()
        {
            List<Card> tempDiscard = new List<Card>(discard);
            discard.RemoveRange(0, discard.Count);
            return tempDiscard;
        }

        public void DiscardCards(Card discardedCard)
        {
            discard.Add(discardedCard);
            UpdateText();
        }

        void UpdateText()
        {
            text.text = discard.Count.ToString();
        }

        public void ShowcaseDiscard()
        {
            FindObjectOfType<CardsShowcase>().ShowcaseCards(null, discard);
        }
    }
}