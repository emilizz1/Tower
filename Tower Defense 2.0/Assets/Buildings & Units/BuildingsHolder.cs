using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers.Buildings
{
    [CreateAssetMenu(menuName = ("Tower defense/Building"))]
    public class BuildingsHolder : ScriptableObject
    {
        [SerializeField] Buildings[] buildings;

        public Buildings GetBuilding(int stage)
        {
            return buildings[stage];
        }
    }
}