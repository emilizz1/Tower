using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Towers.Resources;

namespace Towers.Events
{
    public class SellResourcesEvent : MonoBehaviour
    {
        [SerializeField] Image sellingResource;
        [SerializeField] Image goldImage;
        [SerializeField] Text sellingNumberText;

        int sellingNumber;
        Resource sellingResourceType;

        void Start()
        {
            GetRandomizableResources();
            GetSellingNumber();
            sellingResource.sprite = sellingResourceType.GetSprite();
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
            Resource[] randomizableResources = new Resource[activeResourceSlots.Length -1];
            int randomizableResourceCount = 0;
            for (int i = 0; i < activeResourceSlots.Length; i++)
            {
                if (resourceHolder.ConvertToResource(goldImage.sprite) != resourceHolder.ConvertToResource(activeResourceSlots[i].GetComponentInChildren<Image>().sprite))
                {
                    var activeResourceImage = activeResourceSlots[i].GetComponentInChildren<Image>().sprite;
                    randomizableResources[randomizableResourceCount] = resourceHolder.ConvertToResource(activeResourceImage);
                    randomizableResourceCount++;
                }
            }
            sellingResourceType = randomizableResources[Random.Range(0, randomizableResources.Length)];
        }

        public void Activated()
        {
            var resourceManager = FindObjectOfType<ResourcesManager>();
            Resource[] sellingResources = new Resource[sellingNumber];
            Resource[] gettingGold = new Resource[sellingNumber * 2];
            Resource goldResource = FindObjectOfType<ResourceHolder>().ConvertToResource(goldImage.sprite);
            for (int i = 0; i < sellingNumber; i++)
            {
                sellingResources[i] = sellingResourceType;
                gettingGold[i] = goldResource;
                gettingGold[i + sellingNumber] = goldResource;
            }
            resourceManager.CheckForResources(sellingResources);
            resourceManager.AddResources(gettingGold, transform);
            FindObjectOfType<EventManager>().gameObject.SetActive(false);
        }
    }
}