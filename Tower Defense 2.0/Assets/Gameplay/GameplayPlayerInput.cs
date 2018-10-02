using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayPlayerInput : MonoBehaviour
{
    CameraArm myCamera;
    CardManager cardM;
    BuildingManager buildingM;
    UnitPlacementManager placementM;
    EnemySpawner enemySpawner;
    LevelManager levelM;

    int currentBuilding = 0;
    bool nextStep = true;
    bool placingUnit = false;
    bool firstRound = true;
    State currentState = State.buildingSelecting;

    enum State
    {
        buildingSelecting,
        enemySelecting,
        buildingBonuses,
        placingUnit,
        levelCompleted,
        nothing
    }

	void Start ()
    {
        myCamera = FindObjectOfType<CameraArm>();
        cardM = FindObjectOfType<CardManager>();
        buildingM = FindObjectOfType<BuildingManager>();
        placementM = FindObjectOfType<UnitPlacementManager>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        levelM = FindObjectOfType<LevelManager>();
	}
	
	void Update ()
    {
        if (Input.GetButtonDown("Choice Left"))
        {
            currentChoiceSelected(0);
        }
        if (Input.GetButtonDown("Choice Right"))
        {
            currentChoiceSelected(1);
        }
	}

    void currentChoiceSelected(int choice)
    {
        switch (currentState)
        { //TODO idea! switch enemy and building selections
            case State.buildingSelecting:
                StopCoroutine(WaitingForEnemiesToDie());
                cardM.CardSelected(choice, true);
                if (firstRound)
                {
                    cardM.CardSelected(choice, false);
                    currentState = State.buildingBonuses;
                    myCamera.ViewBuilding(buildingM.GetBulding(currentBuilding).transform.position);
                    buildingM.TurnBuildings(true, true);
                }
                else
                {
                    currentState = State.enemySelecting;
                }
                break;
            case State.enemySelecting:
                cardM.CardSelected(choice, false);
                currentState = State.buildingBonuses;
                myCamera.ViewBuilding(buildingM.GetBulding(currentBuilding).transform.position);
                buildingM.TurnBuildings(true, true);
                break;
            case State.buildingBonuses:
                nextStep = true; placingUnit = false;
                buildingM.BuildingBonusChoosen(choice, currentBuilding, ref nextStep, ref placingUnit);
                if (!nextStep)
                {
                    break;
                }
                else if (placingUnit)
                {
                    myCamera.ViewBattleField();
                    currentState = State.placingUnit;
                    placementM.PrepareForPlacement(currentBuilding);
                    buildingM.TurnBuildings(false, false);
                    
                    break;
                }
                else
                {
                    IsAnotherBuildingAvailable();
                }
                
                break;
            case State.placingUnit:
                placementM.PlaceUnit(choice, currentBuilding);
                IsAnotherBuildingAvailable();
                break;
            case State.levelCompleted:
                SceneManager.LoadSceneAsync(3);
                break;
            case State.nothing:
                break;
        }
    }

    void IsAnotherBuildingAvailable()
    {
        if (buildingM.GetBuildingsLengt() > currentBuilding + 1)
        {
            buildingM.TurnBuildings(true,false);
            currentBuilding++;
            currentState = State.buildingBonuses;
            myCamera.ViewBuilding(buildingM.GetBulding(currentBuilding).transform.position);
        }
        else
        {
            buildingM.TurnBuildings(false,false);
            currentBuilding = 0;
            if (firstRound)
            {
                currentState = State.buildingSelecting;
                cardM.TurnCards(true);
                firstRound = false;
                myCamera.Viewlevel();
            }
            else
            {
                myCamera.ViewBattleField();
                enemySpawner.StartNextWave();
                StartCoroutine(WaitingForEnemiesToDie());
                currentState = State.nothing;
            }
        }
    }

    private IEnumerator WaitingForEnemiesToDie()
    {
        while (!enemySpawner.AreEnemiesAlive())
        {
            yield return new WaitForSecondsRealtime(0.5f);
        }
        CheckForLevelCompleted();
        currentState = State.buildingSelecting;
        cardM.TurnCards(true);
        levelM.LevelFinished();
        myCamera.Viewlevel();
    }

    void CheckForLevelCompleted()
    {
        print("checking " + levelM.CheckForLevelWon());
        if (levelM.CheckForLevelWon())
        {
            print("completed");
            currentState = State.levelCompleted;
            FindObjectOfType<WiningRewards>().PrepareRewards();
        }
        else
        {
            return;
        }
    }
}