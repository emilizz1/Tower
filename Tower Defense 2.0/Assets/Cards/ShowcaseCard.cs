using UnityEngine;
using UnityEngine.UI;

namespace Towers.Cards
{
    public class ShowcaseCard : MonoBehaviour
    {
        [Header("Resources")]
        [SerializeField] Sprite[] resources;

        [Header("Building Setup")]
        [SerializeField] RawImage buildingImage;
        [SerializeField] Text buildingName;
        [SerializeField] Text production;
        [SerializeField] Image productionImage;

        [Header("Unit Setup")]
        [SerializeField] RawImage unitImage;
        [SerializeField] Text unitName;
        [SerializeField] Text unitStats;
        [SerializeField] GameObject[] costImages0;
        [SerializeField] GameObject[] costImages1;
        [SerializeField] GameObject[] costImages2;

        [Header("Enemy Setup")]
        [SerializeField] RawImage enemyImage;
        [SerializeField] Text enemyName;
        [SerializeField] Text enemyAmount;
        [SerializeField] Text enemyStats;
        [SerializeField] Text goldGained;

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
            production.text = settingBuilding.GetResourceAmount().ToString();
            productionImage.sprite = settingBuilding.GetResource();
            SetupUnitCard(settingBuilding);
        }

        public void SetupUnitCard(Buildings building)
        {
            unitImage.texture = building.GetUnitTexture();
            unitName.text = building.GetUnit().name;
            float attack = 0f, speed = 0f, range = 0f;
            building.GetUnit().GetComponent<FriendlyAI>().GiveStats(out attack, out speed, out range);
            unitStats.text = "Attack: " + attack.ToString() + " Speed: " + ((speed * -10f) + 20f).ToString() + " Range: " + range.ToString() + " Special Power: " + building.GetSpecialPower();
            int[] unitCost = building.GetBuildingUnitCost();
            PutUnitResourcesOn(costImages0, unitCost[0], 0);
            PutUnitResourcesOn(costImages1, unitCost[1], 1);
            PutUnitResourcesOn(costImages2, unitCost[2], 2);
        }

        void PutUnitResourcesOn(GameObject[] objects, int cost, int currentResource)
        {
            foreach (GameObject resource in objects)
            {
                if (cost > 0)
                {
                    resource.SetActive(true);
                    resource.GetComponentsInChildren<Image>()[0].sprite = resources[currentResource];
                    cost--;
                }
                else
                {
                    resource.SetActive(false);
                }
            }
        }

        void SetupEnemyCard()
        {
            var enemy = myCard.GetEnemyPrefab();
            enemyImage.texture = myCard.GetEnemyTexture();
            enemyName.text = enemy.name.ToString();
            enemyAmount.text = myCard.GetEnemyAmount().ToString();
            enemyStats.text = enemy.GetComponent<HealthSystem>().GetMaxHP().ToString();
            goldGained.text = myCard.GetEnemyGoldAmount().ToString();
        }
    }
}