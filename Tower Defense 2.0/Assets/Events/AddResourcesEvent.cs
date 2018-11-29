﻿using Towers.Resources;
using UnityEngine;

namespace Towers.Events
{
    public class AddResourcesEvent : MonoBehaviour
    {
        [SerializeField] Sprite[] resourcesSprites;

        public void Activated()
        {
            var resourceHolder = FindObjectOfType<ResourcesManager>();
            resourceHolder.AddResources(2, resourcesSprites[0], transform);
            resourceHolder.AddResources(2, resourcesSprites[1], transform);
            resourceHolder.AddResources(2, resourcesSprites[2], transform);
        }
    }
}