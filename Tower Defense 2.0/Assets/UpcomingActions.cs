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

    bool firstTurn = true;
    bool lastTurn = false;

    GameObject[] states;

    void Start ()
    {

	}
	
	void Update ()
    {
		
	}

    void PrepareNextLevel()
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
            currentPosition -= 15;
        }
        foreach (GameObject state in states)
        {
            var tempPos = state.transform.position;
            tempPos.x = currentPosition;
            state.transform.position = tempPos;
            currentPosition += objectsWidh;
        }
    }

    void AddAllbuildingBonusesIcons()
    {

    }
}
