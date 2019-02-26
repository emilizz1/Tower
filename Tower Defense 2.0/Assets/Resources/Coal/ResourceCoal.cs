using UnityEngine;

namespace Towers.Resources
{
    [CreateAssetMenu(menuName = ("Tower defense/ResourceCoal"))]
    public class ResourceCoal : Resource
    {
        public override void AddResource()
        {
            FindObjectOfType<ResourceHolder>().AddCoal(1);
        }

        public override void RemoveResource()
        {
            FindObjectOfType<ResourceHolder>().AddCoal(-1);
        }
    }
}