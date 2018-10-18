using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpcomingActions : MonoBehaviour
{
    bool firstTurn = true;

    List<State> states = new List<State>();

    enum State
    {
        buildingSelecting,
        enemySelecting,
        enemyWave,
        buildingBonuses,
        levelCompleted,
    }

    void Start ()
    {

	}
	
	void Update ()
    {
		
	}

    void PrepareNextLevel()
    {
        if (firstTurn)
        {
            states.Add(State.buildingSelecting);
            states.Add(State.buildingBonuses);
            firstTurn = false;
        }
        states.Add(State.buildingSelecting);
        states.Add(State.enemySelecting);
        states.Add(State.buildingBonuses);
        states.Add(State.enemyWave);
    }
}
