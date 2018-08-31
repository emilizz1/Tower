using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] Cards card;

    ResourcesManager rM;
    Buildings[] buildings = new Buildings[10];
    int currentBuildingLengh = 0;

    void Start()
    {
        rM = FindObjectOfType<ResourcesManager>();
    }

    public void AddBuilding(Buildings building, bool isItFirst)
    {
        if (!isItFirst)
        {
            for (int i = 0; i < currentBuildingLengh; i++)
            {
                if (buildings[i].GetID() == building.GetID())
                {
                    buildings[i] = building;
                }
            }
        }
        else
        {
            buildings[currentBuildingLengh++] = building;
        }
    }

    public int GetBuildingsLengt()
    {
        return currentBuildingLengh;
    }

    public void BuildingBonusChoosen(int choice, int buildingNumber, ref bool nextStep, ref bool placingUnit)
    {
        if(choice == 0)
        {
            int[] cost = buildings[buildingNumber].GetBuildingUnitCost();
            
            if (rM.CheckForResources(cost[0], cost[1], cost[2]))
            {
                placingUnit = true;
            }
            else
            {
                nextStep = false;
                return;
            }
        }
        else if(choice == 1)
        {
            rM.AddResources(buildings[buildingNumber].GetResourceAmount(), buildings[buildingNumber].GetResource());
            //resource adding animation
        }
        if (buildingNumber + 1 < currentBuildingLengh)
        {
            PutInformation(buildingNumber + 1);
        }
    }

    public void TurnBuildings(bool isItOn, bool firstBuilding)
    {
        card.SetupCards(false, false, isItOn);
        if (firstBuilding)
        {
            PutInformation(0);
        }
    }

    void PutInformation(int buildingNumber)
    {
        card.SetupUnitCard(buildings[buildingNumber], false);
        card.SetupResourceCard(buildings[buildingNumber]);
    }

    public Buildings GetBulding(int selected)
    {
        return buildings[selected];
    }
}
