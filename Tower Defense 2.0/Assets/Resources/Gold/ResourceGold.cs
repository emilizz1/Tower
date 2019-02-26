using UnityEngine;

namespace Towers.Resources
{
    [CreateAssetMenu(menuName = ("Tower defense/ResourceGold"))]
    public class ResourceGold : Resource
    {
        public override void AddResource()
        {
            FindObjectOfType<ResourceHolder>().AddGold(1);
        }

        public override void RemoveResource()
        {
            FindObjectOfType<ResourceHolder>().AddGold(-1);
        }
    }
}