using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
