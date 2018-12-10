using System;
using UnityEngine;

namespace Towers.Resources
{
    public class ResourceHolder : MonoBehaviour
    {
        [SerializeField] Resource[] startingResources;

        int currentGold;
        int currentWood;
        int currentCoal;

        void Awake()
        {
            foreach (Resource resource in startingResources)
            {
                resource.AddResource();
            }
        }

        public int getCurrentGold()
        {
            return currentGold;
        }

        public int getCurrentWood()
        {
            return currentWood;
        }

        public int getCurrentCoal()
        {
            return currentCoal;
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
    }
}