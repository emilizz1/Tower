using System.Collections;
using Towers.BuildingsN;
using Towers.CameraUI;
using Towers.CardN;
using Towers.Enemies;
using Towers.Resources;
using Towers.Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Towers.Scenes
{
    public class GameplayPlayerInput : MonoBehaviour
    {
        CameraArm myCamera;
        CardManager cardM;
        BuildingManager buildingM;
        UnitPlacementManager placementM;
        EnemySpawner enemySpawner;
        LevelManager levelM;
        UpcomingActions upcomingActions;

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

        void Start()
        {
            myCamera = FindObjectOfType<CameraArm>();
            cardM = FindObjectOfType<CardManager>();
            buildingM = FindObjectOfType<BuildingManager>();
            placementM = FindObjectOfType<UnitPlacementManager>();
            enemySpawner = FindObjectOfType<EnemySpawner>();
            levelM = FindObjectOfType<LevelManager>();
            upcomingActions = FindObjectOfType<UpcomingActions>();
        }

        void Update()
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
            {
                case State.buildingSelecting:
                    StopCoroutine(WaitingForEnemiesToDie());
                    cardM.CardSelected(choice, true);
                    if (firstRound)
                    {
                        cardM.CardSelected(choice, false);
                        currentState = State.buildingBonuses;
                        myCamera.ViewBuilding(buildingM.GetBulding(currentBuilding).transform.position);
                        buildingM.TurnBuildings(true, true);
                        upcomingActions.PhaseFinished();
                    }
                    else
                    {
                        currentState = State.enemySelecting;
                        upcomingActions.PhaseFinished();
                    }
                    break;
                case State.enemySelecting:
                    cardM.CardSelected(choice, false);
                    currentState = State.buildingBonuses;
                    upcomingActions.PhaseFinished();
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
            if (buildingM.GetBuildingsLength() > currentBuilding + 1)
            {
                buildingM.TurnBuildings(true, false);
                currentBuilding++;
                currentState = State.buildingBonuses;
                myCamera.ViewBuilding(buildingM.GetBulding(currentBuilding).transform.position);
                upcomingActions.PhaseFinished();
            }
            else
            {
                buildingM.TurnBuildings(false, false);
                currentBuilding = 0;
                if (firstRound)
                {
                    currentState = State.buildingSelecting;
                    cardM.TurnCards(true);
                    firstRound = false;
                    myCamera.Viewlevel();
                    upcomingActions.PhaseFinished();
                }
                else
                {
                    myCamera.ViewBattleField();
                    enemySpawner.StartNextWave();
                    StartCoroutine(WaitingForEnemiesToDie());
                    currentState = State.nothing;
                    upcomingActions.PhaseFinished();
                }
            }
        }

        private IEnumerator WaitingForEnemiesToDie()
        {
            while (!enemySpawner.AreEnemiesAlive())
            {
                yield return new WaitForSecondsRealtime(0.5f);
            }
            if (!CheckForLevelCompleted())
            {
                currentState = State.buildingSelecting;
                cardM.TurnCards(true);
                upcomingActions.NewLevel(false);
            }
            else
            {
                upcomingActions.NewLevel(false);
            }
            levelM.LevelFinished();
            myCamera.Viewlevel();
        }

        bool CheckForLevelCompleted()
        {
            if (levelM.CheckForLevelWon())
            {
                currentState = State.levelCompleted;
                FindObjectOfType<WiningRewards>().PrepareRewards();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}