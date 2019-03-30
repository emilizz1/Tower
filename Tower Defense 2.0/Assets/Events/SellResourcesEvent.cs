using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Towers.Resources;

namespace Towers.Events
{
    public class SellResourcesEvent : MonoBehaviour
    {
        [SerializeField] GameObject sellingResource;
        [SerializeField] GameObject firstGoldResourceImage, secondGoldResourceImage;
        [SerializeField] Text sellingNumberText;

        int sellingNumber;
        Resource sellingResourceType;

        void Start()
        {
            GetRandomizableResources();
            GetSellingNumber();
        }

        void GetSellingNumber()
        {
            sellingNumber = FindObjectOfType<ResourceHolder>().getCurrentResources(sellingResourceType);
            sellingNumberText.text = sellingNumber.ToString() + "x";
        }

        void GetRandomizableResources()
        {
            var resourceHolder = FindObjectOfType<ResourceHolder>();
            var activeResourceSlots = FindObjectOfType<ResourceSetter>().GetActiveResourceSlots();
            Resource[] randomizableResources = new Resource[activeResourceSlots.Length];
            for (int i = 0; i < activeResourceSlots.Length; i++)
            {
                if (firstGoldResourceImage.GetComponentInChildren<Image>().sprite != activeResourceSlots[i].GetComponentInChildren<Image>().sprite)
                {
                    var activeResourceImage = activeResourceSlots[i].GetComponentInChildren<Image>().sprite;
                    randomizableResources[i] = resourceHolder.ConvertToResource(activeResourceImage);
                }
            }
            sellingResourceType = randomizableResources[Random.Range(0, randomizableResources.Length)];
        }

        public void Activated()
        {
            var resourceManager = FindObjectOfType<ResourcesManager>();
            Resource[] sellingResources = new Resource[sellingNumber];
            Resource[] gettingGoldI = new Resource[sellingNumber];
            Resource[] gettingGoldII = new Resource[sellingNumber];
            Resource goldResource = FindObjectOfType<ResourceHolder>().ConvertToResource(firstGoldResourceImage.GetComponentInChildren<Image>().sprite);
            for (int i = 0; i < sellingNumber; i++)
            {
                sellingResources[i] = sellingResourceType;
                gettingGoldI[i] = goldResource;
                gettingGoldII[i] = goldResource;
            }
            resourceManager.CheckForResources(sellingResources);
            resourceManager.AddResources(gettingGoldI, firstGoldResourceImage.transform);
            resourceManager.AddResources(gettingGoldII, secondGoldResourceImage.transform);
            FindObjectOfType<EventManager>().gameObject.SetActive(false);
        }
    }
}