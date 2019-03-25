using Towers.Resources;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Towers.Events
{
    public class AddResourcesEvent : MonoBehaviour
    {
        [SerializeField] Image[] resourcesSlots;

        List<Resource> resourcesToAdd = new List<Resource>();

        void Start()
        {
            GatherResources();
        }

        void GatherResources()
        {
            Resource[] randomizableResources = GetRandomizableResources();
            foreach (Image resource in resourcesSlots)
            {
                Resource randomizedResource = randomizableResources[Random.Range(0, randomizableResources.Length)];
                resource.sprite = randomizedResource.GetSprite();
                resourcesToAdd.Add(randomizedResource);
            }
        }

        Resource[] GetRandomizableResources()
        {
            var resourceHolder = FindObjectOfType<ResourceHolder>();
            var activeResourceSlots = FindObjectOfType<ResourceSetter>().GetActiveResourceSlots();
            Resource[] randomizableResources = new Resource[activeResourceSlots.Length];
            for (int i = 0; i < activeResourceSlots.Length; i++)
            {
                var activeResourceImage = activeResourceSlots[i].GetComponentInChildren<Image>().sprite;
                randomizableResources[i] = resourceHolder.ConvertToResource(activeResourceImage);
            }
            return randomizableResources;
        }

        public void Activated()
        {
            var resourceHolder = FindObjectOfType<ResourcesManager>();
            resourceHolder.AddResources(resourcesToAdd.ToArray(), transform);
            FindObjectOfType<EventManager>().gameObject.SetActive(false);
        }
    }
}