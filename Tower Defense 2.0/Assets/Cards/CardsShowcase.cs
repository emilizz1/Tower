using System.Collections.Generic;
using Towers.BuildingsN;
using UnityEngine;
using Towers.Resources;
using Towers.Core;

namespace Towers.CardN
{
    public class CardsShowcase : MonoBehaviour
    {
        [SerializeField] GameObject cardsUI, showcase, arrowLeft, arrowRight, mouseBlock;
        [SerializeField] ShowcaseCard[] cards;
        [SerializeField] GameObject[] pages;
        
        int activeCards = 0;
        int currentlyActivePage = 0;
        GameObject[] activePages;
        Showing nowShowing = Showing.Nothing;
        List<Buildings> buildings = new List<Buildings>();
        public static CardsShowcase control;

        public enum Showing
        {
            PlayerCards,
            Deck,
            Discard,
            StartLevelCards,
            Nothing
        }

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

        public void ShowcaseCards(CardHolder cardHolder, Showing showing)
        {
            ShowcaseCards(cardHolder, null, showing);
        }

        public void ShowPlayerCards(CardHolder cardHolder)
        {
            if (!FindObjectOfType<MovingResource>())
            {
                ShowcaseCards(cardHolder, null, Showing.PlayerCards);
            }
        }

        public void ShowcaseCards(CardHolder cardHolder, List<Card> cards, Showing showing)
        {
            if (nowShowing != showing)
            {
                Time.timeScale = 0f;
                cardsUI.SetActive(false);
                showcase.SetActive(true);
                InsertCardsInfo(cardHolder, cards);
                buildings = new List<Buildings>();
                nowShowing = showing;
            }
            else
            {
                Time.timeScale = 1f;
                bool eventFinished = FindObjectOfType<SaveLoad>().LoadIntInfo("EventFinished") == 0;
                cardsUI.SetActive(eventFinished);
                showcase.SetActive(false);
                if (nowShowing == Showing.StartLevelCards)
                {

                    FindObjectOfType<CardManager>().Setup();
                }
                nowShowing = Showing.Nothing;
            }
            if(showing == Showing.StartLevelCards)
            {
                mouseBlock.SetActive(false);
            }
            else
            {
                mouseBlock.SetActive(true);
            }
        }

        void InsertCardsInfo(CardHolder cardHolder = null, List<Card> cards = null)
        {
            if (cardHolder != null)
            {
                foreach (Card card in cardHolder.GetAllCards())
                {
                    SetupCard(card);
                }
            }
            else
            {
                foreach (Card card in cards)
                {
                    SetupCard(card);
                }
            }
            SetActivePages();
            TurnOffCards();
        }

        void SetActivePages()
        {
            int activePageCount = (activeCards / 5) + 1;
            if(activeCards% 5 == 0)
            {
                activePageCount--;
            }
            activePages = new GameObject[activePageCount];
            for (int i = 0; i < activePageCount; i++)
            {
                activePages[i] = pages[i];
            }
            activePages[0].SetActive(true);
            CheckIfArrowsActive();
        }

        private void SetupCard(Card card)
        {
            cards[activeCards].gameObject.SetActive(true);
            cards[activeCards].PutInformation(card, GetBuildingLevel(card));
            activeCards++;
        }

        void TurnOffCards()
        {
            for (int i = activeCards; i < cards.Length; i++)
            {
                cards[i].gameObject.SetActive(false);
            }
            activeCards = 0;
        }

        int GetBuildingLevel(Card card)
        {
            int buildingLevel = 0;
            Buildings currentlyLooking = card.GetPrefabs().GetBuilding(0);
            foreach (Buildings building in buildings)
            {
                if (building == currentlyLooking)
                {
                    buildingLevel++;
                }
            }
            buildings.Add(currentlyLooking);
            return buildingLevel;
        }

        public void TurnPage(bool right)
        {
            foreach (GameObject page in activePages)
            {
                page.SetActive(false);
            }
            if (right)
            {
                currentlyActivePage++;
                activePages[currentlyActivePage].SetActive(true);
            }
            else if (!right)
            {
                currentlyActivePage--;
                activePages[currentlyActivePage].SetActive(true);
            }
            CheckIfArrowsActive();
        }

        void CheckIfArrowsActive()
        {
            if(currentlyActivePage != 0)
            {
                arrowLeft.SetActive(true);
            }
            else
            {
                arrowLeft.SetActive(false);
            }
            if (currentlyActivePage + 1 != activePages.Length)
            {
                arrowRight.SetActive(true);
            }
            else
            {
                arrowRight.SetActive(false);
            }
        }

        public void StartLevelAnimation()
        {
            print("Started");
            GetComponent<Animator>().SetTrigger("AddCards");
            Time.timeScale = 1f;
        }

        public void AnimationFinished()
        {
            ShowcaseCards(new CardHolder(), Showing.StartLevelCards);
            FindObjectOfType<CardManager>().SetNewCards(true);
        }
    }
}