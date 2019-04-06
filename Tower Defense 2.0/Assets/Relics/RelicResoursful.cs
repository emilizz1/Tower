using UnityEngine;
using Towers.Resources;

namespace Towers.Relics
{
    public class RelicResoursful : Relic
    {
        [SerializeField] Resource[] resourcesToAdd;

        public override void UseRelic()
        {
            base.UseRelic();
            FindObjectOfType<ResourcesManager>().AddResources(resourcesToAdd, transform);
        }
    }
}