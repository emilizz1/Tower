using UnityEngine;
using System.Collections.Generic;

namespace Towers.Resources
{
    public class ResourceHolder : MonoBehaviour
    {
        public static ResourceHolder instance;

        [SerializeField] Resource[] startingResources;
        [SerializeField] Resource goldResource;
        [SerializeField] Resource woodResource;
        [SerializeField] Resource coalResource;

        int currentGold;
        int currentWood;
        int currentCoal;

        private void Awake()
        {
            instance = this;
        }

        public void ResetResources()
        {
            currentGold = 0;
            currentWood = 0;
            currentCoal = 0;
            foreach (Resource resource in startingResources)
            {
                resource.AddResource();
            }
        }

        public int getCurrentResources(Resource resource)
        {
            if (resource == goldResource)
            {
                return currentGold;
            }
            else if(resource == woodResource)
            {
                return currentWood;
            }
            else if (resource == coalResource)
            {
                return currentCoal;
            }
            return 0;
        }

        public int getCurrentResources(Sprite resource)
        {
            if (resource == goldResource.GetSprite())
            {
                return currentGold;
            }
            else if (resource == woodResource.GetSprite())
            {
                return currentWood;
            }
            else if (resource == coalResource.GetSprite())
            {
                return currentCoal;
            }
            return 0;
        }

        public Resource ConvertToResource(Sprite sprite)
        {
            if (sprite == goldResource.GetSprite())
            {
                return goldResource;
            }
            else if (sprite == woodResource.GetSprite())
            {
                return woodResource;
            }
            else if (sprite == coalResource.GetSprite())
            {
                return coalResource;
            }
            return null;
        }

        //Negative amount to remove
        public void AddGold(int Amount)
        {
            currentGold += Amount;
        }

        //Negative amount to remove
        public void AddWood(int Amount)
        {
            currentWood += Amount;
        }

        //Negative amount to remove
        public void AddCoal(int Amount)
        {
            currentCoal += Amount;
        }

        public List<Resource> GetAllCurrentResources()
        {
            List<Resource> currentResources = new List<Resource>();
            for (int i = 0; i < currentGold; i++)
            {
                currentResources.Add(goldResource);
            }
            for (int i = 0; i < currentWood; i++)
            {
                currentResources.Add(woodResource);
            }
            for (int i = 0; i < currentCoal; i++)
            {
                currentResources.Add(coalResource);
            }
            return currentResources;
        }

        public void SetAllNewResources(List<Resource> resources)
        {
            currentGold = 0;
            currentWood = 0;
            currentCoal = 0;
            foreach(Resource resource in resources)
            {
                resource.AddResource();
            }
        }
    }
}