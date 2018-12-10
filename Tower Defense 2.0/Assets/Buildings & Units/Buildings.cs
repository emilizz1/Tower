using UnityEngine;
using Towers.Resources;

namespace Towers.BuildingsN
{
    public class Buildings : MonoBehaviour
    {
        [SerializeField] string buildingName;
        [SerializeField] Resource[] buildUnitCost;
        [SerializeField] GameObject unitPrefab;
        [SerializeField] Resource[] resourceAmountProduced;
        [SerializeField] int Level;
        [SerializeField] int iD;
        [SerializeField] Texture buildingImage;
        [SerializeField] string SpecialPower;
        [SerializeField] Texture unitsImage;

        public Resource[] GetBuildingUnitCost()
        {
            return buildUnitCost;
        }

        public GameObject GetUnit()
        {
            return unitPrefab;
        }

        public Resource[] GetResourcesProduced()
        {
            return resourceAmountProduced;
        }

        public int GetID()
        {
            return iD;
        }

        public int GetBuildingLevel()
        {
            return Level;
        }

        public Texture GetBuildingTexture()
        {
            return buildingImage;
        }
        public Texture GetUnitTexture()
        {
            return unitsImage;
        }

        public string GetBuildingName()
        {
            return buildingName;
        }

        public string GetSpecialPower()
        {
            return SpecialPower;
        }
    }
}