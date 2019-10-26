using Towers.BuildingsN;
using Towers.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Towers.CameraUI.UpcomingAction
{
    public class UpcomingActions : MonoBehaviour
    {
        [SerializeField] GameObject buildingSelectingIcon;
        [SerializeField] GameObject enemySelectingIcon;
        [SerializeField] GameObject enemyWaveIcon;
        [SerializeField] GameObject buildingBonusesIcon;
        [SerializeField] GameObject levelComnpletedIcon;
        [SerializeField] float objectsWidh = 30f;
        [SerializeField] float objectElevation = 30f;

        bool myFirstTurn = true;

        BuildingManager buildingManager;
        GameObject[] states;
        int currentlyActive = 0;
        int currentlyEmpty = 0;

        public void StartUpcomingActions()
        {
            buildingManager = FindObjectOfType<BuildingManager>();
            NewLevel(true);
        }

        void PrepareLevel()
        {
            ClearAllObjects();
            states = new GameObject[20];
            currentlyEmpty = 0;
            if (myFirstTurn)
            {
                states[currentlyEmpty++] = Instantiate(buildingSelectingIcon, transform);
                AddAllbuildingBonusesIcons(true);
            }
            states[currentlyEmpty++] = Instantiate(buildingSelectingIcon, transform);
            states[currentlyEmpty++] = Instantiate(enemySelectingIcon, transform);
            AddAllbuildingBonusesIcons(false);
            states[currentlyEmpty++] = Instantiate(enemyWaveIcon, transform);
            if (FindObjectOfType<LevelManager>().CheckForLevelWon())
            {
                states[currentlyEmpty++] = Instantiate(levelComnpletedIcon, transform);
            }
            ArrangeStates();
        }

        void ArrangeStates()
        {
            float currentPosition = -objectsWidh * ((currentlyEmpty - 1f) / 2f);
            if (currentlyEmpty % 2 == 0)
            {
                currentPosition -= objectsWidh / 2f;
            }
            for (int i = 0; i < currentlyEmpty; i++)
            {
                var tempPos = states[i].transform.position;
                tempPos.x = currentPosition;
                tempPos.y = 0f;
                tempPos.z = 0f;
                states[i].transform.localPosition = tempPos;
                currentPosition += objectsWidh;
            }
        }

        void AddAllbuildingBonusesIcons(bool firstTurn)
        {
            for (int i = 0; i < buildingManager.GetBuildingsLength(); i++)
            {
                Buildings myBuilding = buildingManager.GetBulding(i);
                states[currentlyEmpty++] = Instantiate(buildingBonusesIcon, transform);
                var myBuildingBonusIcon = states[currentlyEmpty - 1].GetComponent<BuildingBonusIcon>();
                myBuildingBonusIcon.PutInformation(myBuilding.GetResourcesProduced(), myBuilding.GetBuildingUnitCost(), myBuilding.GetBuildingName());
                if (firstTurn) { return; }
            }
        }

        public void NewLevel(bool firstTurn)
        {
            if (!firstTurn)
            {
                currentlyActive = 0;
            }
            myFirstTurn = firstTurn;
            PrepareLevel();
            states[currentlyActive].transform.localPosition = new Vector3(states[currentlyActive].transform.localPosition.x, objectElevation);
        }

        public void PhaseFinished()
        {
            PrepareLevel();
            currentlyActive++;
            states[currentlyActive].transform.localPosition = new Vector3(states[currentlyActive].transform.localPosition.x, objectElevation);
        }

        void ClearAllObjects()
        {
            foreach (Image myObject in GetComponentsInChildren<Image>())
            {
                Destroy(myObject.gameObject);
            }
        }

        public void DestroyUpcomingActions()
        {
            gameObject.SetActive(false);
        }
    }
}