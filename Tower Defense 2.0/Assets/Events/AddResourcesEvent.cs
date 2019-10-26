using Towers.Resources;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Towers.Events
{
    public class AddResourcesEvent : MonoBehaviour
    {
        [SerializeField] Image[] resourcesSlots;
        [SerializeField] Resource[] randomizableResources;

        List<Resource> resourcesToAdd = new List<Resource>();

        void Start()
        {
            GatherResources();
        }

        void GatherResources()
        {
            foreach (Image resource in resourcesSlots)
            {
                Resource randomizedResource = randomizableResources[Random.Range(0, randomizableResources.Length)];
                resource.sprite = randomizedResource.GetSprite();
                resourcesToAdd.Add(randomizedResource);
            }
        }

        public void Activated()
        {
            FindObjectOfType<ResourcesManager>().AddResources(resourcesToAdd.ToArray(), transform);
            FindObjectOfType<EventManager>().gameObject.SetActive(false);
        }
    }
}