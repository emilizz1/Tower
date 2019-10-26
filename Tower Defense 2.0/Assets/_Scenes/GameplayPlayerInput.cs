using System.Collections;
using Towers.BuildingsN;
using Towers.CameraUI;
using Towers.CardN;
using Towers.Enemies;
using Towers.Resources;
using Towers.Units;
using UnityEngine;
using Towers.Core;
using Towers.CameraUI.UpcomingAction;

namespace Towers.Scenes
{
    public class GameplayPlayerInput : MonoBehaviour
    {
        CameraArm myCamera;
        CardManager cardM;
        BuildingManager buildingM;
        UnitPlacementManager placementM;
        EnemySpawner enemySpawner;
        UpcomingActions upcomingActions;
        StartLevelDisplayer startLevelDisplayer;

        int currentBuilding = 0;
        bool nextStep = true;
        bool placingUnit = false;
        bool firstRound = true;
        State currentState = State.LevelStarted;
        float screenWidth;

        enum State
        {
            LevelStarted,
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
            buildingM = FindObjectOfType<BuildingManager>();
            placementM = FindObjectOfType<UnitPlacementManager>();
            enemySpawner = FindObjectOfType<EnemySpawner>();
            cardM = FindObjectOfType<CardManager>();
            upcomingActions = FindObjectOfType<UpcomingActions>();
            startLevelDisplayer = FindObjectOfType<StartLevelDisplayer>();
            buildingM.TurnBuildings(false, false);
            screenWidth = Screen.width;
        }

        void Update()
        {
            if (Input.GetButtonDown("Choice Left"))
            {
                currentChoiceSelected(0);
            }
            else if (Input.GetButtonDown("Choice Right"))
            {
                currentChoiceSelected(1);
            }
        }

        public void MousePressed()
        {
            if (Input.mousePosition.x < screenWidth / 2)
            {
                currentChoiceSelected(0);
            }
            else if (Input.mousePosition.x > screenWidth / 2)
            {
                currentChoiceSelected(1);
            }
        }

        void currentChoiceSelected(int choice)
        {
            switch (currentState)
            {
                case State.LevelStarted: //showing what cards are added to deck
                    startLevelDisplayer.TurnOffAllShowcaseCards();
                    upcomingActions.StartUpcomingActions();
                    currentState = State.buildingSelecting;
                    break;
                case State.buildingSelecting: //selected what building to build
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
                        cardM.SetNewCards(false);
                        upcomingActions.PhaseFinished();
                    }
                    break;
                case State.enemySelecting: //selecting what enemies to add to wave
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
                    if (FindObjectsOfType<MovingResource>().Length == 0)
                    {
                        SaveProgesss();
                        FindObjectOfType<LoadLevel>().LoadScene(3);
                    }
                    break;
                case State.nothing:
                    break;
            }
        }

        void SaveProgesss()
        {
            var saveLoad = FindObjectOfType<SaveLoad>();
            saveLoad.SavePlayerResources();
            saveLoad.SaveIntInfo("Lifepoints", FindObjectOfType<LifePoints>().GetLifePoints());
            saveLoad.SaveCompletedLevels(FindObjectOfType<LevelCounter>().GetLevelFinished());
            saveLoad.SaveIntInfo("EventFinished", 0);
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
                    cardM.SetNewCards(true);
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
                cardM.SetNewCards(true);
                cardM.TurnCards(true);
                upcomingActions.NewLevel(false);
            }
            else
            {
                upcomingActions.NewLevel(false);
            }
            LevelManager.instance.WaveFinished();
            myCamera.Viewlevel();
        }

        bool CheckForLevelCompleted()
        {
            if (LevelManager.instance.CheckForLevelWon())
            {
                currentState = State.levelCompleted;
                FindObjectOfType<WiningRewards>().PrepareRewards();
                upcomingActions.DestroyUpcomingActions();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}