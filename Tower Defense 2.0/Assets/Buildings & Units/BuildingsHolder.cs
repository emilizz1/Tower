using UnityEngine;

namespace Towers.BuildingsN
{
    [CreateAssetMenu(menuName = ("Tower defense/Building"))]
    public class BuildingsHolder : ScriptableObject
    {
        [SerializeField] Buildings[] buildings;

        public Buildings GetBuilding(int stage)
        {
            print("Asking for building stage: " + stage + " of " + buildings[0].gameObject.name);
            return buildings[stage];
        }
    }
}