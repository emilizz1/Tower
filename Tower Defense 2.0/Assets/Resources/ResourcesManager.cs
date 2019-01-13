using System.Collections;
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

        void Start()
        {
            goldSprite = goldImage.sprite;
            woodSprite = woodImage.sprite;
            coalSprite = coalImage.sprite;
            resourceHolder = FindObjectOfType<ResourceHolder>();
            updateResourceText();
        }

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
            gold.text = resourceHolder.getCurrentGold().ToString();
            wood.text = resourceHolder.getCurrentWood().ToString();
            coal.text = resourceHolder.getCurrentCoal().ToString();
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
            if(resource.GetSprite() == goldSprite) { return goldImage.transform.position; }
            else if(resource.GetSprite() == woodSprite) { return woodImage.transform.position; }
            else if(resource.GetSprite() == coalSprite) { return coalImage.transform.position; }
            else { return Vector3.zero; }
        }

        public bool CheckForResources(Resource[] resources)
        {
            int gold = 0, wood = 0, coal = 0;
            GetResourcesAmounts(resources, out gold, out wood, out coal);
            if (gold <= resourceHolder.getCurrentGold() && wood <= resourceHolder.getCurrentWood() && coal <= resourceHolder.getCurrentCoal())
            {
                StartCoroutine(PayResources(resources));
                return true;
            }
            else
            {
                return false;
            }
        }

        void GetResourcesAmounts(Resource[] resources, out int gold, out int wood, out int coal)
        {
            gold = 0;
            wood = 0;
            coal = 0;
            foreach (Resource resource in resources)
            {
                if (resource.GetSprite() == goldSprite) { gold++; }
                else if (resource.GetSprite() == woodSprite) { wood++; }
                else if (resource.GetSprite() == coalSprite) { coal++; }
            }
        }

        IEnumerator PayResources(Resource[] resources)
        {
            foreach (Resource resource in resources)
            {
                GameObject createdResource = Instantiate(resourceImage, GetResourcePos(resource), Quaternion.identity, transform);
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

        Vector3 GetResourcePos(Resource resource)
        {
            if (resource.GetSprite() == goldSprite) { return goldImage.transform.position; }
            else if (resource.GetSprite() == woodSprite) { return woodImage.transform.position; }
            else if (resource.GetSprite() == coalSprite) { return coalImage.transform.position; }
            else { return Vector3.zero; }
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
        }
    }
}