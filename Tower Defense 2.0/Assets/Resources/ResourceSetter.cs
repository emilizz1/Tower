using System.Collections.Generic;
using UnityEngine;
using Towers.CardN;
using UnityEngine.UI;
using System;

namespace Towers.Resources
{
    public class ResourceSetter : MonoBehaviour
    {
        [SerializeField] GameObject[] resourceSlots;

        int activeResourceSlotAmount = 0;

        void Awake()
        {
            FindObjectOfType<ResourcesManager>().GiveActiveResourceSlots(resourceSlots);
        }

        internal GameObject[] GetActiveResourceSlots()
        {
            return resourceSlots;
        }
    }
}