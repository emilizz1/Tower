using Towers.BuildingsN;
using Towers.CardN;
using Towers.Enemies;
using Towers.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Towers.Resources
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
            lifepointText.text = lifepointsRewarded.ToString();
            FindObjectOfType<LifePoints>().DamageLifePoints(-lifepointsRewarded);
            DisplayResourceRewards();
            FindObjectOfType<LevelCounter>().LevelFinished(currentLevel);
            if (cardAddedToDeck != null)
            {
                cardRewards.SetActive(true);
                FindObjectOfType<CardHolders>().AddPlayerCard(cardAddedToDeck);
                GetComponentInChildren<ShowcaseCard>().PutInformation(cardAddedToDeck, GetBuildingLevel());
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

        void DisplayResourceRewards()
        {
            DeactivateAllResourceRewards();
            var resourceManager = FindObjectOfType<ResourcesManager>();
            int currentlyUsedResourceSlot = 0;
            Resource lastResource = null;
            foreach (Resource resource in resourcesAwarded)
            {
                if(resource != lastResource)
                {
                    Resource[] resources = resourceManager.CountAllResourcesOfType(resource, resourcesAwarded);
                    resourcesGameObjects[currentlyUsedResourceSlot].SetActive(true);
                    resourcesGameObjects[currentlyUsedResourceSlot].GetComponentInChildren<Image>().sprite = resource.GetSprite();
                    resourcesGameObjects[currentlyUsedResourceSlot].GetComponentInChildren<Text>().text = resources.Length.ToString();
                    resourceManager.AddResources(resources, resourcesGameObjects[currentlyUsedResourceSlot].transform);
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
                if (currentlyLooking == card.GetPrefabs().GetBuilding(0))
                {
                    buildingLevel++;
                }
            }
            return buildingLevel + 1;
        }
    }
}