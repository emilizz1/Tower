using UnityEngine;
using UnityEngine.UI;
using Towers.Resources;

namespace Towers.CameraUI
{
    public class BuildingBonusIcon : MonoBehaviour
    {
        [SerializeField] Image buildingProductionImage;
        [SerializeField] Text buildingProductionAmount;
        [SerializeField] Text[] unitCostText;
        [SerializeField] Text buildingName;

        public void PutInformation(Resource[] buildingResource, Resource[] UnitCost, string buildingName)
        {
            buildingProductionImage.sprite = buildingResource[0].GetSprite();
            buildingProductionAmount.text = buildingResource.Length.ToString();
            DisableUnitCostText();
            PutResourcesInformation(UnitCost);
            this.buildingName.text = buildingName;
        }

        private void PutResourcesInformation(Resource[] UnitCost)
        {
            ResourcesManager resourcesManager = FindObjectOfType<ResourcesManager>();
            int currentlyUsedResourceSlot = 0;
            Resource lastResource = null;
            foreach (Resource resource in UnitCost)
            {
                if (resource != lastResource)
                {
                    unitCostText[currentlyUsedResourceSlot].gameObject.SetActive(true);
                    Resource[] resources = resourcesManager.CountAllResourcesOfType(resource, UnitCost);
                    unitCostText[currentlyUsedResourceSlot].text = resources.Length.ToString();
                    unitCostText[currentlyUsedResourceSlot].GetComponentInChildren<Image>().sprite = resource.GetSprite();
                    currentlyUsedResourceSlot++;
                }
                lastResource = resource;
            }
        }

        void DisableUnitCostText()
        {
            foreach(Text text in unitCostText)
            {
                text.gameObject.SetActive(false);
            }
        }
    }
}