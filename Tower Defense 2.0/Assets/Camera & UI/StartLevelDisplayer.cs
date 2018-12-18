using System.Collections.Generic;
using UnityEngine;
using Towers.CardN;
using Towers.BuildingsN;

namespace Towers.CameraUI
{
    public class StartLevelDisplayer : MonoBehaviour
    {
        [SerializeField] ShowcaseCard[] showcaseCards;
        [SerializeField] GameObject text;

        List<Buildings> buildings = new List<Buildings>();
        CardHolder levelCards;

        void Start()
        {
            levelCards = FindObjectOfType<Deck>().GetLevelCards();
            DisplayLevelCards();
        }

        void DisplayLevelCards()
        {
            int currentlyShowing = 0;
            TurnOffAllShowcaseCards();
            text.SetActive(true);
            foreach (Card card in levelCards.GetAllCards())
            {
                showcaseCards[currentlyShowing].gameObject.SetActive(true);
                showcaseCards[currentlyShowing].PutInformation(card, GetBuildingLevel(card));
                currentlyShowing++;
            }
        }

        public void TurnOffAllShowcaseCards()
        {
            foreach (ShowcaseCard showcaseCard in showcaseCards)
            {
                showcaseCard.gameObject.SetActive(false);
            }
            text.SetActive(false);
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
    }
}