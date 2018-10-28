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

    void Start ()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
	}

    public void PrepareLevel()
    {
        states = null;
        if (firstTurn)
        {
            states[states.Length] = buildingSelectingIcon;
            AddAllbuildingBonusesIcons();
            firstTurn = false;
        }
        else
        {
            states[states.Length] = buildingSelectingIcon;
            states[states.Length] = enemySelectingIcon;
            AddAllbuildingBonusesIcons();
            states[states.Length] = enemyWaveIcon;
            if (lastTurn)
            {
                states[states.Length] = levelComnpletedIcon;
            }
            else
            {
                states[states.Length] = nextTurnIcon;
            }
        }
    }

    void ArrangeStates()
    {
        float currentPosition = -objectsWidh * ((states.Length - 1f) / 2f);
        if (states.Length % 2 == 0)
        {
            currentPosition -= objectsWidh /2f;
        }
        foreach (GameObject state in states)
        {
            var tempPos = state.transform.position;
            tempPos.x = currentPosition;
            state.transform.position = tempPos;
            //why?
            currentPosition += objectsWidh;
        }
    }

    void AddAllbuildingBonusesIcons()
    {
        for (int i = 0; i < buildingManager.GetBuildingsLength(); i++)
        {
            Buildings myBuilding = buildingManager.GetBulding(i);
            var myBuildingBonusIcon = Instantiate(buildingBonusesIcon, transform). GetComponent<BuildingBonusIcon>();
            myBuildingBonusIcon.PutInformation(myBuilding.GetResource(), myBuilding.GetResourceAmount(), myBuilding.GetBuildingUnitCost(), myBuilding.name);
        }
    }

    public void NewLevel()
    {
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
    }
}
