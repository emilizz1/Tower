using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacementManager : MonoBehaviour
{
    [SerializeField] GameObject friendlyHolder;
    [SerializeField] GameObject[] placementPositionsLeft;
    [SerializeField] GameObject[] placementPositionsRight;
    [SerializeField] ParticleSystem whitePS;
    [SerializeField] ParticleSystem redPS;
    [SerializeField] GameObject colorChoice;

    BuildingManager buildingM;
    int whiteLengh;
    int redLengh;
    
    private void Start()
    {
        buildingM = FindObjectOfType<BuildingManager>();
        whiteLengh = placementPositionsLeft.Length;
        redLengh = placementPositionsRight.Length;
    }

    public void PlaceUnit(int choice, int currentBuilding)
    {
        GameObject unit = Instantiate(buildingM.GetBulding(currentBuilding).GetUnit());
        if (choice == 0) {
            unit.transform.position = whitePS.transform.position;
            RemovePosition(whitePS.transform.position, true);
        }
        else if (choice == 1)
        {
            unit.transform.position = redPS.transform.position;
            RemovePosition(redPS.transform.position, false);
        }
        unit.GetComponent<FriendlyAI>().SetPossition();
        unit.gameObject.layer = 0;
        unit.transform.parent = friendlyHolder.transform;
        whitePS.gameObject.SetActive(false);
        redPS.gameObject.SetActive(false);
        colorChoice.SetActive(false);
    }

    public void PrepareForPlacement()   
    {
        whitePS.gameObject.SetActive(true);
        redPS.gameObject.SetActive(true);
        whitePS.transform.position = placementPositionsLeft[Random.Range(0, whiteLengh)].transform.position;
        redPS.transform.position = placementPositionsRight[Random.Range(0, redLengh)].transform.position;
        colorChoice.SetActive(true);
    }

    void RemovePosition(Vector3 position, bool Left)
    {
        if (Left)
        {
            for (int i = 0; i < whiteLengh; i++)
            {
                if(position == placementPositionsLeft[i].transform.position)
                {
                    placementPositionsLeft[i] = placementPositionsLeft[whiteLengh-1];
                    whiteLengh--;
                }
            }
        }
        else
        {
            for (int i = 0; i < redLengh; i++)
            {
                if (position == placementPositionsRight[i].transform.position)
                {
                    
                    placementPositionsRight[i] = placementPositionsRight[redLengh-1];
                    redLengh--;
                }
            }
        }
    }
}