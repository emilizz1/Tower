using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buildings : MonoBehaviour
{
    [SerializeField] string buildingName;
    [Tooltip("Gold, Wood, Coal")][SerializeField] int[] buildUnitCost;
    [SerializeField] GameObject unitPrefab;
    [SerializeField] int resourceAmount;
    [SerializeField] Sprite resource;
    [SerializeField] int Level;
    [SerializeField] int iD;
    [SerializeField] Texture buildingImage;
    [SerializeField] string SpecialPower;
    [SerializeField] Texture unitsImage;

    public int[] GetBuildingUnitCost()
    {
        return buildUnitCost;
    }

    public GameObject GetUnit()
    {
        return unitPrefab;
    }

    public int GetResourceAmount()
    {
        return resourceAmount;
    } 

    public Sprite GetResource()
    {
        return resource;
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
