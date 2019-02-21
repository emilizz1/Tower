using System.Collections.Generic;
using Towers.BuildingsN;
using UnityEngine;

namespace Towers.CardN
{
    public class CardsShowcase : MonoBehaviour
    {
        [SerializeField] GameObject cardsUI;
        [SerializeField] GameObject showcase;
        [SerializeField] ShowcaseCard[] cards;
        [SerializeField] GameObject[] pages;
        [SerializeField] GameObject arrowLeft;
        [SerializeField] GameObject arrowRight;
        
        int currentlyActiveCards = 0;
        int currentlyActivePage = 0;
        GameObject[] activePages;
        bool showing = false;

        List<Buildings> buildings = new List<Buildings>();
        public static CardsShowcase control;

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

        public void ShowcaseCards(CardHolder cardHolder)
        {
            if (!showing)
            {
                Time.timeScale = 0f;
                cardsUI.SetActive(false);
                showcase.SetActive(true);
                InsertCardsInfo(cardHolder);
                CheckIfArrowsActive();
            }
            else
            {
                Time.timeScale = 1f;
                cardsUI.SetActive(true);
                showcase.SetActive(false);
            }
        }

        void InsertCardsInfo(CardHolder cardHolder)
        {
            foreach (Card card in cardHolder.GetAllCards())
            {
                SetupCard(card);
            }
            SetActivePages();
            TurnOffCards();
        }

        void SetActivePages()
        {
            int activePageCount = (currentlyActiveCards / 5) + 1;
            activePages = new GameObject[activePageCount];
            for (int i = 0; i < activePageCount; i++)
            {
                activePages[i] = pages[i];
            }
            activePages[0].SetActive(true);
        }

        private void SetupCard(Card card)
        {
            cards[currentlyActiveCards].gameObject.SetActive(true);
            cards[currentlyActiveCards].PutInformation(card, GetBuildingLevel(card));
            currentlyActiveCards++;
        }

        void TurnOffCards()
        {
            for (int i = currentlyActiveCards; i < cards.Length; i++)
            {
                cards[i].gameObject.SetActive(false);
            }
            currentlyActiveCards = 0;
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
    }
}