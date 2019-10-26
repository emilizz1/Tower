using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Towers.Resources
{
    public class ResourcesManager : MonoBehaviour
    {
        [SerializeField] GameObject resourceImage;
        [SerializeField] float resourceGatherSpeed = 15f;
        [SerializeField] float dissapearingResourceOffset = -200f;

        GameObject[] resourceSlots;
        Transform deliveringFrom;

        void Update()
        {
                updateResourceText();
        }

        public void updateResourceText()
        {
            for (int i = 0; i < resourceSlots.Length; i++)
            {
                resourceSlots[i].GetComponentInChildren<Text>().text = ResourceHolder.instance.getCurrentResources(resourceSlots[i].GetComponentInChildren<Image>().sprite).ToString();
            }
        }
        
        public void AddResources(Resource[] Resources, Transform carryFrom = null, bool attachToTransform = false)
        {
            if(carryFrom == null)
            {
                carryFrom = FindObjectOfType<ResourceCardChoice>().transform;
            }
            StartCoroutine(GatherResources(Resources, carryFrom, attachToTransform));
        }

        IEnumerator GatherResources(Resource[] Resources, Transform carryFrom, bool attachToTransform)
        {
            foreach (Resource resource in Resources)
            {
                GameObject createdResource;
                //if (attachToTransform) // When delivering from higher sorting layer canvas
                //{
                //    createdResource = Instantiate(resourceImage, carryFrom.position, Quaternion.identity, carryFrom);
                //}
                //else
                //{
                    createdResource = Instantiate(resourceImage, carryFrom.position, Quaternion.identity, transform);
                //}
                createdResource.GetComponent<Image>().sprite = resource.GetSprite();
                createdResource.AddComponent<MovingResource>();
                createdResource.GetComponent<MovingResource>().GiveResourceMovementInfo(GetResourceDestination(resource), resourceGatherSpeed, resource);
                createdResource.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                yield return new WaitForSeconds(0.1f);
                updateResourceText();
            }
        }

        Transform GetResourceDestination(Resource resource)
        {
            for (int i = 0; i < resourceSlots.Length; i++)
            {
                if (resourceSlots[i].GetComponentInChildren<Image>().sprite == resource.GetSprite())
                {
                    return resourceSlots[i].GetComponentInChildren<Image>().transform;
                }
            }
            return null; // if no resource was found
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
                if(ResourceHolder.instance.getCurrentResources(resourceSlots[i].GetComponentInChildren<Image>().sprite) < currentResource) { return false; }
            }
            StartCoroutine(PayResources(resources));
            return true;
        }

        IEnumerator PayResources(Resource[] resources)
        {
            foreach (Resource resource in resources)
            {
                GameObject createdResource = Instantiate(resourceImage, GetResourceDestination(resource).position, Quaternion.identity, transform);
                createdResource.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
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
                createdResource.transform.position = Vector3.MoveTowards(createdResource.transform.position, target, resourceGatherSpeed / 3 * Time.deltaTime);
                createdResImageColor.a = createdResImageColor.a - 0.05f;
                createdResource.GetComponent<Image>().color = createdResImageColor;
                yield return new WaitForSeconds(0.05f);
                target = new Vector3(createdResource.transform.position.x, createdResource.transform.position.y + dissapearingResourceOffset, 0f);
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
            updateResourceText();
        }

        public bool CheckIfResourceIsActive(Resource resource)
        {
            for (int i = 0; i < resourceSlots.Length; i++)
            {
                if(resourceSlots[i].GetComponentInChildren<Image>().sprite == resource.GetSprite())
                {
                    return true;
                }
            }
            return false;
        }
    }
}