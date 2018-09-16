using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Image[] costImages;
    [SerializeField] Text[] costText;

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
        int nextFreeCostSlot = 0;
        for (int i = 0; i < 3; i++)
        {
            costImages[i].enabled = false;
            costText[i].enabled = false;
            if (unitCost[i] != 0)
            {
                costImages[nextFreeCostSlot].enabled = true;
                costImages[nextFreeCostSlot].sprite = resources[i];
                costText[nextFreeCostSlot].enabled = true;
                costText[nextFreeCostSlot].text = unitCost[i].ToString();
                nextFreeCostSlot++;
            }
        }
    }

    void SetupEnemyCard()
    {
        var enemy = myCard.GetEnemyPrefab();
        enemyImage.texture = myCard.GetEnemyTexture();
        enemyName.text = enemy.name.ToString();
        enemyAmount.text = "Amount: " + myCard.GetEnemyAmount().ToString();
        enemyStats.text = "Health: " + enemy.GetComponent<HealthSystem>().GetMaxHP().ToString() + " Speed: " + enemy.GetComponent<Character>().GetMovementSpeed().ToString();
        goldGained.text = myCard.GetEnemyGoldAmount().ToString();
    }
}
