using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpcomingActions : MonoBehaviour
{
    [SerializeField] GameObject buildingSelectingIcon;
    [SerializeField] GameObject enemySelectingIcon;
    [SerializeField] GameObject enemyWaveIcon;
    [SerializeField] GameObject buildingBonusesIcon;
    [SerializeField] GameObject levelComnpletedIcon;
    [SerializeField] GameObject nextTurnIcon;

    [SerializeField] float objectsWidh = 30f;
    [SerializeField] float objectSize = 1.3f;
    [SerializeField] float objectElevation = 30f;

    bool firstTurn = true;
    bool lastTurn = false;

    BuildingManager buildingManager;
    GameObject[] states;
    int currentlyActive = 0;
    int currentlyEmpty = 0;

    void Start ()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        NewLevel();
	}

    public void PrepareLevel()
    {
        states = new GameObject[20] ;
        currentlyEmpty = 0;
        if (firstTurn)
        {
            states[currentlyEmpty++] = Instantiate(buildingSelectingIcon, transform);
            AddAllbuildingBonusesIcons();
            firstTurn = false;
        }
            states[currentlyEmpty++] = Instantiate(buildingSelectingIcon, transform);
            states[currentlyEmpty++] = Instantiate(enemySelectingIcon, transform);
            AddAllbuildingBonusesIcons();
            states[currentlyEmpty++] = Instantiate(enemyWaveIcon, transform);
            if (lastTurn)
            {
                states[currentlyEmpty++] = Instantiate(levelComnpletedIcon, transform);
            }
            else
            {
                states[currentlyEmpty++] = Instantiate(nextTurnIcon, transform);
            }
        ArrangeStates();
    }

    void ArrangeStates()
    {
        float currentPosition = -objectsWidh * ((currentlyEmpty - 1f) / 2f);
        if (currentlyEmpty % 2 == 0)
        {
            currentPosition -= objectsWidh /2f;
        }
        for (int i = 0; i < currentlyEmpty; i++)
        {
            var tempPos = states[i].transform.position;
            tempPos.x = currentPosition;
            states[i].transform.position = tempPos;
            currentPosition += objectsWidh;
        }
    }

    void AddAllbuildingBonusesIcons()
    {
        for (int i = 0; i < buildingManager.GetBuildingsLength(); i++)
        {
            Buildings myBuilding = buildingManager.GetBulding(i);
            var myBuildingBonusIcon = Instantiate(buildingBonusesIcon, transform).GetComponent<BuildingBonusIcon>();
            myBuildingBonusIcon.PutInformation(myBuilding.GetResource(), myBuilding.GetResourceAmount(), myBuilding.GetBuildingUnitCost(), myBuilding.name);
        }
    }

    public void NewLevel()
    {
        CleanAllObjects();
        currentlyActive = 0;
        PrepareLevel();
        states[currentlyActive].transform.localScale = new Vector3(objectSize, objectSize, objectSize);
        states[currentlyActive].transform.localPosition = new Vector3(states[currentlyActive].transform.position.x, objectElevation);
    }

    public void PhaseFinished()
    {
        var lastState = states[currentlyActive];
        var currentState = states[currentlyActive + 1];
        currentlyActive++;
        lastState.transform.localScale = new Vector3(1f, 1f, 1f);
        currentState.transform.localScale = new Vector3(objectSize, objectSize, objectSize);
        lastState.transform.localPosition = new Vector3(lastState.transform.position.x, 0f);
        currentState.transform.localPosition = new Vector3(currentState.transform.position.x, objectElevation);

        print(states[2].transform.position);
    }

    void CleanAllObjects()
    {
        for (int i = 0; i < currentlyActive; i++)
        {
            Destroy(states[i].gameObject);
        }
    }
}
