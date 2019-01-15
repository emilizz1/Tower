﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Towers.Resources
{
    public class ResourcesManager : MonoBehaviour
    {
        [SerializeField] GameObject resourceImage;
        [SerializeField] float resourceMoveSpeed = 10f;
        [SerializeField] float dissapearingResourceOffset = -200f;

        GameObject[] resourceSlots;
        Transform deliveringFrom;
        ResourceHolder resourceHolder;

        void Update()
        {
            if (resourceHolder == null)
            {
                resourceHolder = FindObjectOfType<ResourceHolder>();
                updateResourceText();
            }
        }

        public void updateResourceText()
        {
            for (int i = 0; i < resourceSlots.Length; i++)
            {
                resourceSlots[i].GetComponentInChildren<Text>().text = resourceHolder.getCurrentResources(resourceSlots[i].GetComponentInChildren<Image>().sprite).ToString();

            }
        }
        
        public void AddResources(Resource[] Resources, Transform cardTransform = null)
        {
            if(cardTransform == null)
            {
                cardTransform = FindObjectOfType<ResourceCardChoice>().transform;
            }
            StartCoroutine(GatherResources(Resources, cardTransform));
        }

        IEnumerator GatherResources(Resource[] Resources, Transform carryFrom)
        {
            foreach (Resource resource in Resources)
            {
                var createdResource = Instantiate(resourceImage, carryFrom.position, Quaternion.identity, transform);
                createdResource.GetComponent<Image>().sprite = resource.GetSprite();
                createdResource.AddComponent<MovingResource>();
                createdResource.GetComponent<MovingResource>().GiveResourceMovementInfo(GetResourceDestination(resource), resourceMoveSpeed, resource);
                yield return new WaitForSecondsRealtime(0.15f);
                updateResourceText();
            }
        }

        Vector3 GetResourceDestination(Resource resource)
        {
            for (int i = 0; i < resourceSlots.Length; i++)
            {
                if (resourceSlots[i].GetComponentInChildren<Image>().sprite == resource.GetSprite())
                {
                    return resourceSlots[i].GetComponentInChildren<Image>().transform.position;
                }
            }
            return Vector3.zero; // if no resource was found
        }

        public bool CheckForResources(Resource[] resources)
        {
            for (int i = 0; i < resourceSlots.Length; i++)
            {
                int currentResource = 0;
                foreach(Resource resource in resources)
                {
                    if(resourceSlots[i].GetComponentInChildren<Image>().sprite == resource.GetSprite())
                    {
                        currentResource++;
                    }
                }
                if(resourceHolder.getCurrentResources(resourceSlots[i].GetComponentInChildren<Image>().sprite) < currentResource) { return false; }
            }
            StartCoroutine(PayResources(resources));
            return true;
        }

        IEnumerator PayResources(Resource[] resources)
        {
            foreach (Resource resource in resources)
            {
                GameObject createdResource = Instantiate(resourceImage, GetResourceDestination(resource), Quaternion.identity, transform);
                createdResource.GetComponent<Image>().sprite = resource.GetSprite();
                resource.RemoveResource();
                updateResourceText();
                StartCoroutine(ResourceDissapearing(createdResource));
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }

        IEnumerator ResourceDissapearing(GameObject createdResource)
        {
            var createdResImageColor = createdResource.GetComponent<Image>().color;
            Vector3 target = new Vector3(createdResource.transform.position.x, createdResource.transform.position.y + dissapearingResourceOffset, 0f);
            while (Vector3.Distance(createdResource.transform.position, target) > 1f)
            {
                createdResource.transform.position = Vector3.MoveTowards(createdResource.transform.position, target, resourceMoveSpeed / 3);
                createdResImageColor.a = createdResImageColor.a - 0.05f;
                createdResource.GetComponent<Image>().color = createdResImageColor;
                yield return new WaitForSeconds(0.05f);
            }
            Destroy(createdResource);
        }

        public Resource[] CountAllResourcesOfType(Resource type, Resource[] resources)
        {
            int howMany = 0;
            foreach (Resource resource in resources)
            {
                if (resource == type)
                {
                    howMany++;
                }
            }
            Resource[] OneTypeResources = new Resource[howMany];
            for (int i = 0; i < howMany; i++)
            {
                OneTypeResources[i] = type;
            }
            return OneTypeResources;
        }

        public void GiveActiveResourceSlots(GameObject[] activeResourceSlots)
        {
            resourceSlots = activeResourceSlots;
            resourceHolder = FindObjectOfType<ResourceHolder>();
            updateResourceText();
        }
    }
}