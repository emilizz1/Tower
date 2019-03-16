using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Towers.CardN
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] CardHolder levelCards;
        [SerializeField] GameObject shuffledCard;

        bool deckPrepared = false;

        Card[] playerCards;
        List<Card> selected = new List<Card>();
        List<Card> deck = new List<Card>();
        Discard discard;
        Text text;

        void PrepareDeck()
        {
            text = GetComponentInChildren<Text>();
            discard = FindObjectOfType<Discard>();
            playerCards = FindObjectOfType<CardHolders>().GetAllPlayerCards();
            ShuffleHolders();
            deckPrepared = true;
        }

        void ShuffleHolders()
        {
            Card[] tempDeck = new Card[playerCards.Length + levelCards.GetAllCards().Length];
            System.Array.Copy(playerCards, tempDeck, playerCards.Length);
            System.Array.Copy(levelCards.GetAllCards(), 0, tempDeck, playerCards.Length, levelCards.GetAllCards().Length);
            deck = tempDeck.ToList<Card>();
        }

        public List<Card> GetNewCards()
        {
            if (!deckPrepared)
            {
                PrepareDeck();
            }
            selected = new List<Card>();
            for (int i = 0; i <= 1; i++)
            {
                if (deck.Count <= 0)
                {
                    //Gets discarded cards
                    var shuffledCards = discard.GetDiscard();
                    ShuffleAnimation(shuffledCards.Count);
                    deck.AddRange(shuffledCards);
                }
                Card tempCard = deck.ToArray()[UnityEngine.Random.Range(0, deck.Count)];

                selected.Add(tempCard);
                deck.Remove(tempCard);
            }
            UpdateText();
            return selected;

        }

        void ShuffleAnimation(int times)
        {
            for (int i = 0; i < times; i++)
            {
                var createdObj = Instantiate(shuffledCard, discard.transform.position, Quaternion.identity, transform);
                createdObj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                createdObj.transform.localPosition = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0f) + createdObj.transform.localPosition;
                createdObj.GetComponent<MovingObject>().GiveMovementInfo(transform.position, Random.Range(0.45f, 0.7f), Random.Range(-5, 5));
            }
        }

        public void RemoveACard(Card cardToRemove)
        {
            selected.Remove(cardToRemove);
        }

        void UpdateText()
        {
            text.text = deck.Count.ToString();
        }

        public CardHolder GetLevelCards()
        {
            return levelCards;
        }

        public void ShowcaseDeck()
        {
            FindObjectOfType<CardsShowcase>().ShowcaseCards(null, deck, CardsShowcase.Showing.Deck);
        }
    }
}