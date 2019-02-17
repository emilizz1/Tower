using Towers.BuildingsN;
using Towers.Enemies;
using Towers.Units;
using UnityEngine;
using UnityEngine.UI;
using Towers.Resources;

namespace Towers.CardN
{
    public class ShowcaseCard : MonoBehaviour
    {
        [Header("Building Setup")]
        [SerializeField] RawImage buildingImage;
        [SerializeField] Text buildingName;
        [SerializeField] Text production;
        [SerializeField] Image productionImage;

        [Header("Unit Setup")]
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
        [SerializeField] RawImage enemyImage;
        [SerializeField] Text enemyName;
        [SerializeField] Text enemyAmount;
        [SerializeField] Text enemyHealth;
        [SerializeField] Text goldGained;
        [SerializeField] Text enemySpecial;

        Card myCard;

        public void PutInformation(Card card, int buildingLevel)
        {
            myCard = card;
            SetupBuildingCard(buildingLevel);
            SetupEnemyCard();
        }

        void SetupBuildingCard(int buildingLevel)
        {
            Buildings settingBuilding = myCard.GetPrefabs().GetBuilding(buildingLevel);
            buildingImage.texture = settingBuilding.GetBuildingTexture();
            buildingName.text = settingBuilding.GetBuildingName();
            production.text = settingBuilding.GetResourcesProduced().Length.ToString();
            productionImage.sprite = settingBuilding.GetResourcesProduced()[0].GetSprite();
            SetupUnitCard(settingBuilding);
        }

        public void SetupUnitCard(Buildings building)
        {
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
            foreach (Resource resource in displayedResources)
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

        void SetupEnemyCard()
        {
            var enemy = myCard.GetEnemyPrefab();
            enemyImage.texture = myCard.GetEnemyTexture();
            enemyName.text = enemy.name.ToString();
            enemyAmount.text = myCard.GetEnemyAmount().ToString();
            enemyHealth.text = enemy.GetComponent<HealthSystem>().GetMaxHP().ToString();
            goldGained.text = myCard.GetEnemyResources().Length.ToString();
            enemySpecial.text = enemy.GetComponent<HealthSystem>().GetSpecial();
        }
    }
}