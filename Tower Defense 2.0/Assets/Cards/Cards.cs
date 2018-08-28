using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] Sprite[] resources;

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
    [SerializeField] Text unitStats;
    [SerializeField] Image[] costImages;
    [SerializeField] Text[] costText;

    [Header("Enemy Setup")]
    [SerializeField] GameObject enemyCard;
    [SerializeField] RawImage enemyImage;
    [SerializeField] Text enemyName;
    [SerializeField] Text enemyAmount;
    [SerializeField] Text enemyStats;
    [SerializeField] Text goldGained;

    [Header("Resource Setup")]
    [SerializeField] GameObject resourceCard;
    [SerializeField] Image resourceImage;
    [SerializeField] Text resourceText;
 
    Card card;

    public BuildingsHolder GetPrefabs()
    {
        return card.GetPrefabs();
    }
	
    public int GetEnemyCardCost()
    {
        return card.GetEnemyGoldAmount();
    }

    public GameObject GetEnemyPrefab()
    {
        return card.GetEnemyPrefab();
    }

    public float GetEnemyAmount()
    {
        return card.GetEnemyAmount();
    }

    public void SetCard(Card cardToSet, bool secondChoice)
    {
        card = cardToSet;
        SetupCards(secondChoice, !secondChoice, false);
        if (secondChoice)
        {
            SetupBuildingCard();
        } 
        else
        {
            SetupEnemyCard();
        }
        
    }

    void SetupEnemyCard()
    {
        var enemy = card.GetEnemyPrefab();
        enemyImage.texture = card.GetEnemyTexture();
        enemyName.text = enemy.name.ToString();
        enemyAmount.text = "Amount: " + card.GetEnemyAmount().ToString();
        enemyStats.text = "Health: " + enemy.GetComponent<HealthSystem>().GetMaxHP().ToString() + " Movement speed: " + enemy.GetComponent<Character>().GetMovementSpeed().ToString();
        goldGained.text = card.GetEnemyGoldAmount().ToString();
    }

    void SetupBuildingCard()
    {
        int buildingLevel = FindObjectOfType<BuildingPlacementManager>().GetBuildingLevel(card.GetPrefabs().GetBuilding(0));
        Buildings settingBuilding = card.GetPrefabs().GetBuilding(buildingLevel);
        buildingImage.texture = settingBuilding.GetBuildingTexture();
        buildingName.text = settingBuilding.GetBuildingName();
        production.text = "Resource: " + settingBuilding.GetResourceAmount().ToString();
        productionImage.sprite = settingBuilding.GetResource();
        SetupUnitCard(settingBuilding, true);

    }

    public void SetupUnitCard(Buildings building, bool whileChoosingBuilding)
    {
        if (whileChoosingBuilding)
        {
            unitsCard.transform.localPosition = new Vector3(unitsCard.transform.localPosition.x, -150f, unitsCard.transform.localPosition.z);
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
        building.GetUnit().GetComponent<FriendlyAI>().GiveStats(out attack, out speed, out range);
        unitStats.text = "Attack: " + attack.ToString() + " Speed: " + ((speed * -10f) + 20f).ToString() + " Range: " + range.ToString() + " Special Power: " + building.GetSpecialPower();
        int[] unitCost = building.GetBuildingUnitCost();
        int nextFreeCostSlot = 0;
        for (int i = 0; i < 3; i++)
        {
            costImages[i].enabled = false;
            costText[i].enabled = false;
            if(unitCost[i] != 0)
            {
                costImages[nextFreeCostSlot].enabled = true;
                costImages[nextFreeCostSlot].sprite = resources[i];
                costText[nextFreeCostSlot].enabled = true;
                costText[nextFreeCostSlot].text = unitCost[i].ToString();
                nextFreeCostSlot++;
            }
        }
    }

    public void SetupCards(bool isItBuilding, bool isItEnemy, bool isItResource)
    {
        if (isItBuilding)
        {
            buildingsCard.SetActive(true);
            unitsCard.SetActive(true);
            enemyCard.SetActive(false);
            resourceCard.SetActive(false);
        }
        else if (isItEnemy)
        {
            buildingsCard.SetActive(false);
            unitsCard.SetActive(false);
            enemyCard.SetActive(true);
            resourceCard.SetActive(false);
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

    public Card GetCard()
    {
        return card;
    }

    public void SetupResourceCard(Buildings building)
    {
        resourceImage.sprite = building.GetResource();
        resourceText.text = building.GetResourceAmount().ToString();
    }
}
