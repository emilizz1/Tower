using Towers.Resources;
using UnityEngine;

namespace Towers.Events
{
    public class AddResourcesEvent : MonoBehaviour
    {
        [SerializeField] Resource[] resourcesToAdd;

        public void Activated()
        {
            var resourceHolder = FindObjectOfType<ResourcesManager>();
            resourceHolder.AddResources(resourcesToAdd, transform);
        }
    }
}