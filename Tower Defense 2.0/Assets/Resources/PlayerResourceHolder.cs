using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers.Resources
{
    [CreateAssetMenu(menuName = ("Tower defense/ResourceHolder"))]
    public class PlayerResourceHolder : ScriptableObject
    {
        List<Resource> resources = new List<Resource>();

        public List<Resource> GetResources()
        {
            return resources;
        }

        public void GetResources(List<Resource> givenResources)
        {
            resources = givenResources;
        }
    }
}
