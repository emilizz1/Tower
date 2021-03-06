﻿using System.Collections;
using Towers.BuildingsN;
using Towers.Enemies;
using Towers.Units;
using UnityEngine;
using UnityEngine.UI;
using Towers.Resources;

namespace Towers.CardN
{
    public class Cards : MonoBehaviour
    {
        [Header("Building Setup")]
        [SerializeField] GameObject buildingsCard;
        [SerializeField] RawImage buildingImage;
        [SerializeField] Text buildingName;
        [SerializeField] Text production;
        [SerializeField] Image productionImage;

        [Header("Unit Setup")]
        [SerializeField] GameObject unitsCard;
        [SerializeField] RawImage unitImage;
        [SerializeField] Text unitName;
        [SerializeField] Text unitAttack;
        [SerializeField] Text unitFireRate;
        [SerializeField] Text unitRange;
        [SerializeField] Text unitSpecial;
        [System.Serializable] class CostResources
        {
            public GameObject[] resourceSlot = null;
        }
        [SerializeField] CostResources[] costImages;

        [Header("Enemy Setup")]
        [SerializeField] GameObject enemyCard;
        [SerializeField] RawImage enemyImage;
        [SerializeField] Text enemyName;
        [SerializeField] Text enemyAmount;
        [SerializeField] Text enemyHealth;
        [SerializeField] Text goldGained;
        [SerializeField] Text enemySpecial;

        [Header("Resource Setup")]
        [SerializeField] GameObject resourceCard;
        [SerializeField] Image[] resourceImages;

        Card card;

        public BuildingsHolder GetPrefabs()
        {
            return card.GetPrefabs();
        }

        public Resource[] GetEnemyResourses()
        {
            return card.GetEnemyResources();
        }

        public GameObject GetEnemyPrefab()
        {
            return card.GetEnemyPrefab();
        }

        public float GetEnemyAmount()
        {
            return card.GetEnemyAmount();
        }

        void SetupEnemyCard()
        {
            FindObjectOfType<LevelEnemyCard>().PutEnemiesOnScreen();
            var enemy = card.GetEnemyPrefab();
            enemyImage.texture = card.GetEnemyTexture();
            enemyName.text = enemy.name.ToString();
            enemyAmount.text = card.GetEnemyAmount().ToString();
            enemyHealth.text = enemy.GetComponent<HealthSystem>().GetMaxHP().ToString();
            goldGained.text = card.GetEnemyResources().Length.ToString();
            enemySpecial.text = enemy.GetComponent<HealthSystem>().GetSpecial();
        }

        void SetupBuildingCard()
        {
            int buildingLevel = FindObjectOfType<BuildingPlacementManager>().GetBuildingLevel(card.GetPrefabs().GetBuilding(0));
            Buildings settingBuilding = card.GetPrefabs().GetBuilding(buildingLevel);
            buildingImage.texture = settingBuilding.GetBuildingTexture();
            buildingName.text = settingBuilding.GetBuildingName();
            production.text = settingBuilding.GetResourcesProduced().Length.ToString();
            productionImage.sprite = settingBuilding.GetResourcesProduced()[0].GetSprite();
            SetupUnitCard(settingBuilding, true);
        }

        public void SetupUnitCard(Buildings building, bool whileChoosingBuilding)
        {
            if (whileChoosingBuilding)
            {
                unitsCard.transform.localPosition = new Vector3(unitsCard.transform.localPosition.x, -175f, unitsCard.transform.localPosition.z); // TODO -175 serialize
                unitsCard.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            else
            {
                unitsCard.transform.localPosition = Vector3.zero;
                unitsCard.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            unitImage.texture = building.GetUnitTexture();
            unitName.text = building.GetUnit().name;
            float attack = 0f, speed = 0f, range = 0f;
            building.GetUnit().GetComponent<Shooter>().GiveStats(out attack, out speed, out range);
            unitAttack.text = attack.ToString();
            unitFireRate.text = Mathf.RoundToInt(10 - 5f * speed).ToString();
            unitRange.text = range.ToString();
            unitSpecial.text = building.GetSpecialPower();
            DisplayUnitCost(building);
        }

        void DisplayUnitCost(Buildings building)
        {
            Resource[] unitCost = building.GetBuildingUnitCost();
            ResourcesManager resourcesManager = FindObjectOfType<ResourcesManager>();
            Resource lastResource = null;
            int currentlyDisplayedResource = 0;
            foreach (Resource resource in unitCost)
            {
                if (resource != lastResource)
                {
                    Resource[] oneTypeResources = resourcesManager.CountAllResourcesOfType(resource, unitCost);
                    DisplayOneTypeResource(costImages[currentlyDisplayedResource].resourceSlot, oneTypeResources);
                    currentlyDisplayedResource++;
                }
                lastResource = resource;
            }
            for (int i = currentlyDisplayedResource; i < costImages.Length; i++)
            {
                InactiveAllResources(costImages[i].resourceSlot, 0);
            }
        }

        void DisplayOneTypeResource(GameObject[] displayOn, Resource[] displayedResources)
        {
            int displayCounter = 0;
            foreach(Resource resource in displayedResources)
            {
                displayOn[displayCounter].gameObject.SetActive(true);
                displayOn[displayCounter].GetComponentsInChildren<Image>()[1].sprite = resource.GetSprite();
                displayCounter++;
            }
            InactiveAllResources(displayOn, displayCounter);
        }

        void InactiveAllResources(GameObject[] inactivateObjects, int lastCount)
        {
            for (int i = lastCount; i < inactivateObjects.Length; i++)
            {
                inactivateObjects[i].SetActive(false);
            }
        }

        public void SetupCards(bool isItBuilding, bool isItEnemy, bool isItResource, Card cardToSet = null)
        {
            if(cardToSet != null)
            {
                card = cardToSet;
            }
            if (FindObjectOfType<CardManager>().GetAnimationIdle())
            {
                if (isItBuilding)
                {
                    buildingsCard.SetActive(true);
                    unitsCard.SetActive(true);
                    enemyCard.SetActive(false);
                    resourceCard.SetActive(false);
                    SetupBuildingCard();
                }
                else if (isItEnemy)
                {
                    buildingsCard.SetActive(false);
                    unitsCard.SetActive(false);
                    enemyCard.SetActive(true);
                    resourceCard.SetActive(false);
                    SetupEnemyCard();
                }
                else if (isItResource)
                {
                    buildingsCard.SetActive(false);
                    unitsCard.SetActive(true);
                    enemyCard.SetActive(false);
                    resourceCard.SetActive(true);
                }
                else
                {
                    buildingsCard.SetActive(false);
                    unitsCard.SetActive(false);
                    enemyCard.SetActive(false);
                    resourceCard.SetActive(false);
                }
            }
            else
            {
                StartCoroutine(WaitForIdleAnimation(isItBuilding, isItEnemy, isItResource));
            }
        }

        IEnumerator WaitForIdleAnimation(bool isItBuilding, bool isItEnemy, bool isItResource)
        {
            while (!FindObjectOfType<CardManager>().GetAnimationIdle())
            {
                yield return new WaitForFixedUpdate();
            }
            SetupCards(isItBuilding, isItEnemy, isItResource);
        }

        public Card GetCard()
        {
            return card;
        }

        public void SetupResourceCard(Buildings building)
        {
            int resourceAmount = building.GetResourcesProduced().Length;
            foreach (Image resource in resourceImages)
            {
                if (resourceAmount > 0)
                {
                    resource.transform.parent.gameObject.SetActive(true);
                    resource.sprite = building.GetResourcesProduced()[0].GetSprite();
                    resourceAmount--;
                }
                else
                {
                    resource.transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }
}