using UnityEngine;
using Towers.CardN;
using System.Collections.Generic;
using Towers.BuildingsN;

namespace Towers.Scenes.RunSelection
{
    public class DeckBuilder : MonoBehaviour
    {
        [SerializeField] GameObject leftChoice, rightChoice;
        [SerializeField] CardHolder addableCards;
        [SerializeField] int cardsToDraft = 6; //make private
 
        CardHolder cardsDrafted;
        Card leftCard, rightCard;
        List<Buildings> buildings = new List<Buildings>();

        void Start()
        {
            cardsDrafted = new CardHolder();
            PrepareNewCards();
        }

        public void CardChosen(int choice)
        {
            if(choice == 0)
            {
                cardsDrafted.AddCard(leftCard);
                buildings.Add(leftCard.GetPrefabs().GetBuilding(0));
                addableCards.AddCard(rightCard);
            }
            else
            {
                cardsDrafted.AddCard(rightCard);
                buildings.Add(rightCard.GetPrefabs().GetBuilding(0));
                addableCards.AddCard(leftCard);
            }
            if (!IsFinished())
            {
                PrepareNewCards();
            }
        }

        public bool IsFinished()
        {
            return cardsDrafted.GetAllCards().Length == cardsToDraft;
        }

        void PrepareNewCards()
        {
            leftCard = addableCards.GetAllCards()[Random.Range(0, addableCards.GetAllCards().Length)];
            leftChoice.GetComponentInChildren<ShowcaseCard>().PutInformation(leftCard, GetBuildingLevel(leftCard));
            addableCards.RemoveCard(leftCard);
            rightCard = addableCards.GetAllCards()[Random.Range(0, addableCards.GetAllCards().Length)];
            rightChoice.GetComponentInChildren<ShowcaseCard>().PutInformation(rightCard, GetBuildingLevel(rightCard));
            addableCards.RemoveCard(rightCard);
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
            return buildingLevel;
        }
    }
}