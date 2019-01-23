using UnityEngine;

namespace Towers.BuildingsN
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