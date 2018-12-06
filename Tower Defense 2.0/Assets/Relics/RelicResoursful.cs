using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Towers.Resources;

namespace Towers.Relics
{
    public class RelicResoursful : Relic
    {
        [SerializeField] int[] resourcesToGive = new int[3];

        public override void UseRelic()
        {
            base.UseRelic();
            FindObjectOfType<ResourcesManager>().AddResources(resourcesToGive[0], resourcesToGive[1], resourcesToGive[2], transform);
        }
    }
}