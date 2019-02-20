using System.Collections.Generic;
using Towers.BuildingsN;
using UnityEngine;

namespace Towers.CardN
{
    public class CardsShowcase : MonoBehaviour
    {
        [SerializeField] CardHolder playerCards;
        [SerializeField] ShowcaseCard[] cards;
        [SerializeField] GameObject[] Pages;
        [SerializeField] GameObject arrowLeft;
        [SerializeField] GameObject arrowRight;
        
        int currentlyActiveCards = 0;
        int currentlyActivePage = 0;

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

        void Start()
        {
            ShowcaseCards();
            CheckIfArrowsActive();
        }

        void ShowcaseCards()
        {
            foreach (Card card in playerCards.GetAllCards())
            {
                SetupCard(card);
            }
            TurnOffCards();
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
            foreach (GameObject page in Pages)
            {
                page.SetActive(false);
            }
            if (right)
            {
                Pages[currentlyActivePage + 1].SetActive(true);
            }
            else if (!right)
            {
                Pages[currentlyActivePage - 1].SetActive(true);
            }
            CheckIfArrowsActive();
        }

        void CheckIfArrowsActive()
        {
            if(Pages[currentlyActivePage - 1] != null)
            {
                arrowLeft.SetActive(true);
            }
            else
            {
                arrowLeft.SetActive(false);
            }
            if (Pages[currentlyActivePage + 1] != null)
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