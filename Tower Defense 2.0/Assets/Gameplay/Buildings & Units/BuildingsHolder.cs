using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Tower defense/Building"))]
public class BuildingsHolder : ScriptableObject
{
    [SerializeField] Buildings[] buildings;

    public Buildings GetBuilding(int stage)
    {
        return buildings[stage];
    }
}
