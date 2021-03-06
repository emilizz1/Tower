﻿using Towers.BuildingsN;
using UnityEngine;

namespace Towers.Units
{
    public class UnitPlacementManager : MonoBehaviour
    {
        [SerializeField] GameObject friendlyHolder;
        [SerializeField] GameObject placementPositionnLeft;
        [SerializeField] GameObject placementPositionnRight;
        [SerializeField] ParticleSystem whitePS;
        [SerializeField] ParticleSystem redPS;
        [SerializeField] GameObject colorChoice;
        [SerializeField] GameObject firstPlacementRange;
        [SerializeField] GameObject secondPlacementRange;

        Transform[] placementPositionsLeft;
        Transform[] placementPositionsRight;
        BuildingManager buildingM;
        int whiteLengh;
        int redLengh;

        private void Start()
        {
            buildingM = FindObjectOfType<BuildingManager>();
            placementPositionsLeft = placementPositionnLeft.GetComponentsInChildren<Transform>();
            placementPositionsRight = placementPositionnRight.GetComponentsInChildren<Transform>();
            whiteLengh = placementPositionsLeft.Length;
            redLengh = placementPositionsRight.Length;
            RemovePosition(Vector3.zero, true);
            RemovePosition(Vector3.zero, false);
        }

        public void PlaceUnit(int choice, int currentBuilding)
        {
            GameObject unit = Instantiate(buildingM.GetBulding(currentBuilding).GetUnit());
            if (choice == 0)
            {
                unit.transform.position = whitePS.transform.position;
                RemovePosition(whitePS.transform.position, true);
            }
            else if (choice == 1)
            {
                unit.transform.position = redPS.transform.position;
                RemovePosition(redPS.transform.position, false);
            }
            unit.GetComponent<Shooter>().SetPossition();
            unit.gameObject.layer = 0;
            unit.transform.parent = friendlyHolder.transform;
            whitePS.gameObject.SetActive(false);
            firstPlacementRange.SetActive(false);
            redPS.gameObject.SetActive(false);
            secondPlacementRange.SetActive(false);
            colorChoice.SetActive(false);
        }

        public void PrepareForPlacement(int currentBuilding)
        {
            float unitRange = buildingM.GetBulding(currentBuilding).GetUnit().GetComponent<Shooter>().GetRange();
            whitePS.gameObject.SetActive(true);
            firstPlacementRange.SetActive(true);
            redPS.gameObject.SetActive(true);
            secondPlacementRange.SetActive(true);
            whitePS.transform.position = placementPositionsLeft[Random.Range(0, whiteLengh)].position;
            firstPlacementRange.transform.position = new Vector3(whitePS.transform.position.x, -1f, whitePS.transform.position.z);
            firstPlacementRange.transform.localScale = new Vector3(unitRange * 2, 1f, unitRange * 2);
            redPS.transform.position = placementPositionsRight[Random.Range(0, redLengh)].position;
            secondPlacementRange.transform.position = new Vector3(redPS.transform.position.x, -1f, redPS.transform.position.z);
            secondPlacementRange.transform.localScale = new Vector3(unitRange * 2, 1f, unitRange * 2);
            colorChoice.SetActive(true);
        }

        void RemovePosition(Vector3 position, bool Left)
        {
            if (Left)
            {
                for (int i = 0; i < whiteLengh; i++)
                {
                    if (position == placementPositionsLeft[i].position)
                    {
                        placementPositionsLeft[i] = placementPositionsLeft[whiteLengh - 1];
                        whiteLengh--;
                    }
                }
            }
            else
            {
                for (int i = 0; i < redLengh; i++)
                {
                    if (position == placementPositionsRight[i].position)
                    {

                        placementPositionsRight[i] = placementPositionsRight[redLengh - 1];
                        redLengh--;
                    }
                }
            }
        }
    }
}