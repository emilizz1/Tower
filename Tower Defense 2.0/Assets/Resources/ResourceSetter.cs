using System.Collections.Generic;
using UnityEngine;
using Towers.CardN;
using UnityEngine.UI;

namespace Towers.Resources
{
    public class ResourceSetter : MonoBehaviour
    {
        [SerializeField] GameObject[] ResourceSlots;

        int activeResourceSlotAmount = 0;

        void Awake()
        {
            var myCards = GatherAllCards();
            var differentResources = GatherDifferentResources(myCards);
            DisplayResources(differentResources);
            DiactivateEmptySlots();
            FindObjectOfType<ResourcesManager>().GiveActiveResourceSlots(GetActiveResourceSlots());
        }

        void DiactivateEmptySlots()
        {
            for (int o = activeResourceSlotAmount; o < ResourceSlots.Length; o++)
            {
                ResourceSlots[o].SetActive(false);
            }
        }

        public GameObject[] GetActiveResourceSlots()
        {
            GameObject[] activeResourceSlots = new GameObject[activeResourceSlotAmount];
            for (int o = 0; o < activeResourceSlotAmount; o++)
            {
                activeResourceSlots[o] = ResourceSlots[o];
            }
            return activeResourceSlots;
        }

        void DisplayResources(List<Resource> resources)
        {
            foreach (Resource resource in resources)
            {
                ResourceSlots[activeResourceSlotAmount].GetComponentInChildren<Image>().sprite = resource.GetSprite();
                ResourceSlots[activeResourceSlotAmount].GetComponentInChildren<Text>().text = FindObjectOfType<ResourceHolder>().getCurrentResources(resource).ToString(); ;
                activeResourceSlotAmount++;
            }
        }

        List<Card> GatherAllCards()
        {
            List<Card> gatheredCards = new List<Card>();
            if (FindObjectOfType<Deck>())
            {
                foreach (Card card in FindObjectOfType<Deck>().GetLevelCards().GetAllCards())
                {
                    gatheredCards.Add(card);
                }
            }
            foreach (Card card in FindObjectOfType<CardHolders>().GetAllPlayerCards())
            {
                gatheredCards.Add(card);
            }
            return gatheredCards;
        }

        List<Resource> GatherDifferentResources(List<Card> myCards)
        {
            List<Resource> differentResources = new List<Resource>();
            foreach (Card card in myCards)
            {
                foreach (Resource resource in card.GetPrefabs().GetBuilding(0).GetBuildingUnitCost())
                {
                    if (!differentResources.Contains(resource))
                    {
                        differentResources.Add(resource);
                    }
                }
            }
            return differentResources;
        }
    }
}