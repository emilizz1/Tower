using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Towers.Resources
{
    [CreateAssetMenu(menuName = ("Tower defense/ResourceWood"))]
    public class ResourceWood : Resource
    {

        public override void AddResource()
        {
            FindObjectOfType<ResourceHolder>().AddWood(1);
        }

        public override void RemoveResource()
        {
            FindObjectOfType<ResourceHolder>().AddWood(-1);
        }
    }
}
