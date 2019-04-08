using Towers.BuildingsN;
using Towers.CardN;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Towers.Resources;
using Towers.Core;

namespace Towers.Scenes
{
    public class WiningRewards : MonoBehaviour
    {
        [SerializeField] GameObject rewards;
        [Header("Lifepoints")]
        [SerializeField] Text lifepointText;
        [SerializeField] int lifepointsRewarded = 5;
        [Header("Resources")]
        [SerializeField] Resource[] resourcesAwarded;
        [SerializeField] GameObject[] resourcesGameObjects;
        [Header("Level")]
        [SerializeField] int currentLevel;
        [Header("Cards")]
        [SerializeField] GameObject cardRewards;
        [SerializeField] Card cardAddedToDeck;
        [SerializeField] Card[] cardsAddedToAddables;

        public void PrepareRewards()
        {
            rewards.SetActive(true);
            StartCoroutine(AddLife());
            DisplayResourceRewards();
            FindObjectOfType<LevelCounter>().LevelFinished(currentLevel);
            if (cardAddedToDeck != null)
            {
                cardRewards.SetActive(true);
                GetComponentInChildren<ShowcaseCard>().PutInformation(cardAddedToDeck, GetBuildingLevel());
                FindObjectOfType<CardHolders>().AddPlayerCard(cardAddedToDeck);
            }
            else
            {
                if (cardRewards)
                {
                    cardRewards.SetActive(false);
                }
            }
            foreach (Card card in cardsAddedToAddables)
            {
                FindObjectOfType<CardHolders>().AddAddableCard(card);
            }
        }

        IEnumerator AddLife()
        {
            lifepointText.text = lifepointsRewarded.ToString();
            for (int i = 0; i < lifepointsRewarded; i++)
            {
                FindObjectOfType<LifePoints>().DamageLifePoints(-1);
                yield return new WaitForSeconds(1.5f / lifepointsRewarded);
            }
        }

        void DisplayResourceRewards()
        {
            DeactivateAllResourceRewards();
            var resourceManager = FindObjectOfType<ResourcesManager>();
            int currentlyUsedResourceSlot = 0;
            Resource lastResource = null;
            foreach (Resource resource in resourcesAwarded)
            {
                if (resource != lastResource && resourceManager.CheckIfResourceIsActive(resource))
                {
                    Resource[] resources = resourceManager.CountAllResourcesOfType(resource, resourcesAwarded);
                    resourcesGameObjects[currentlyUsedResourceSlot].SetActive(true);
                    resourcesGameObjects[currentlyUsedResourceSlot].GetComponentInChildren<Image>().sprite = resource.GetSprite();
                    resourcesGameObjects[currentlyUsedResourceSlot].GetComponentInChildren<Text>().text = resources.Length.ToString();
                    resourceManager.AddResources(resources, resourcesGameObjects[currentlyUsedResourceSlot].transform, true);
                    currentlyUsedResourceSlot++;
                }
                lastResource = resource;
            }
        }

        void DeactivateAllResourceRewards()
        {
            foreach (GameObject myGameobject in resourcesGameObjects)
            {
                myGameobject.SetActive(false);
            }
        }

        int GetBuildingLevel()
        {
            int buildingLevel = 0;
            Buildings currentlyLooking = cardAddedToDeck.GetPrefabs().GetBuilding(0);
            foreach (Card card in FindObjectOfType<CardHolders>().GetAllPlayerCards())
            {
                if (currentlyLooking.GetID() == card.GetPrefabs().GetBuilding(0).GetID())
                {
                    buildingLevel++;
                }
            }
            return buildingLevel;
        }
    }
}