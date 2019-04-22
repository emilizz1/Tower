using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers.Resources;

namespace Towers.Enemies.BlackMarketer
{

    public class BlackMarketer : HealthSystem
    {
        [SerializeField] string special;

        public override IEnumerator KillCharacter()
        {
            GiveGold();
            return base.KillCharacter();
        }

        void GiveGold()
        {
            Resource[] goldResources = new Resource[1];
            goldResources[0] = new ResourceGold();
            FindObjectOfType<ResourcesManager>().AddResources(goldResources);
        }

        public override string GetSpecial()
        {
            return special;
        }
    }
}