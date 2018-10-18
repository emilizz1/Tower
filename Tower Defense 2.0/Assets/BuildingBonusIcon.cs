using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBonusIcon : MonoBehaviour
{
    [SerializeField] Image buildingProductionImage;
    [SerializeField] Text buildingProductionAmount;
    [SerializeField] Text unitGoldText;
    [SerializeField] Text unitWoodText;
    [SerializeField] Text unitCoalText;

    public void PutInformation(Sprite buildingResource, int buildingAmount, int[] UnitCost)
    {
        buildingProductionImage.sprite = buildingResource;
        buildingProductionAmount.text = buildingAmount.ToString();
        if(UnitCost[0] != 0)
        {
            unitGoldText.text = UnitCost[0].ToString();
            unitGoldText.gameObject.SetActive(true);
        }
        else
        {
            unitGoldText.gameObject.SetActive(false);
        }
        if (UnitCost[1] != 0)
        {
            unitWoodText.text = UnitCost[1].ToString();
            unitWoodText.gameObject.SetActive(true);
        }
        else
        {
            unitWoodText.gameObject.SetActive(false);
        }
        if (UnitCost[2] != 0)
        {
            unitCoalText.text = UnitCost[2].ToString();
            unitCoalText.gameObject.SetActive(true);
        }
        else
        {
            unitCoalText.gameObject.SetActive(false);
        }
    }
	
}
