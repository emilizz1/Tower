﻿using UnityEngine;
using Towers.CardN;
using Towers.Resources;

namespace Towers.BuildingsN
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] Cards cardL;
        [SerializeField] Cards cardR;

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

        public int GetBuildingsLength()
        {
            return currentBuildingLengh;
        }

        public void BuildingBonusChoosen(int choice, int buildingNumber, ref bool nextStep, ref bool placingUnit)
        {
            if (choice == 0)
            {
                if (rM.CheckForResources(buildings[buildingNumber].GetBuildingUnitCost()))
                {
                    placingUnit = true;
                }
                else
                {
                    nextStep = false;
                    return;
                }
            }
            else if (choice == 1)
            {
                rM.AddResources(buildings[buildingNumber].GetResourcesProduced());
            }
            if (buildingNumber + 1 < currentBuildingLengh)
            {
                PutInformation(buildingNumber + 1);
            }
        }

        public void TurnBuildings(bool isItOn, bool firstBuilding)
        {
            cardL.SetupCards(false, false, isItOn);
            cardR.SetupCards(false, false, false);
            if (firstBuilding)
            {
                PutInformation(0);
            }
        }

        void PutInformation(int buildingNumber)
        {
            cardL.SetupUnitCard(buildings[buildingNumber], false);
            cardL.SetupResourceCard(buildings[buildingNumber]);
        }

        public Buildings GetBulding(int selected)
        {
            return buildings[selected];
        }
    }
}