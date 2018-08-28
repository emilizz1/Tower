using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesGatherer : MonoBehaviour
{
    ResourcesManager resourceManager;

    void Start()
    {
        resourceManager = GetComponent<ResourcesManager>();
    }

    public void ProduceResource(int amount, Sprite image, Transform transformFrom)
    {

    }
}
