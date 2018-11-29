using Towers.CardN;
using UnityEngine;


namespace Towers.BuildingsN
{
    public class BuildingPlacementManager : MonoBehaviour
    {
        [SerializeField] GameObject placementPositions;
        [SerializeField] GameObject friendllyHolder;
        Transform[] positions;
        int unusedPosition = 1;
        CardManager cardM;
        Buildings oldBuilding;
        BuildingManager bM;

        void Start()
        {
            cardM = FindObjectOfType<CardManager>();
            bM = FindObjectOfType<BuildingManager>();
            positions = placementPositions.GetComponentsInChildren<Transform>();
        }

        public void BuildingSelected()
        {
            Buildings gettingBuilding = cardM.GetPrefabs().GetBuilding(0).GetComponent<Buildings>();
            if (GetBuildingLevel(gettingBuilding) == 0)
            {
                bM.AddBuilding(Instantiate(cardM.GetPrefabs().GetBuilding(0), positions[unusedPosition++].position, cardM.GetPrefabs().GetBuilding(0).transform.rotation, friendllyHolder.transform), true);
            }
            else if (GetBuildingLevel(gettingBuilding) > 0)
            {
                Buildings newBuilding = cardM.GetPrefabs().GetBuilding(oldBuilding.GetBuildingLevel() + 1);
                bM.AddBuilding(Instantiate(newBuilding, oldBuilding.transform.position, newBuilding.transform.rotation, friendllyHolder.transform), false);
                Destroy(oldBuilding.gameObject);
            }
        }

        public int GetBuildingLevel(Buildings building)
        {
            int currentLevel = -1;
            foreach (Buildings obj in friendllyHolder.GetComponentsInChildren<Buildings>())
            {
                if (obj.GetID() == building.GetID())
                {
                    oldBuilding = obj;
                    currentLevel = obj.GetBuildingLevel();
                }
            }
            return ++currentLevel;
        }
    }
}