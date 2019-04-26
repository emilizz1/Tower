using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers.Resources;

namespace Towers.Enemies.BlackMarketer
{

    public class BlackMarketer : HealthSystem
    {
        [SerializeField] string special;
        [SerializeField] Resource[] resouces;

        public bool GiveGold = false;

        private void Update()
        {
            if (GiveGold)
            {
                FindObjectOfType<ResourcesManager>().AddResources(resouces);

                GiveGold = false;
            }
        }

        public override IEnumerator KillCharacter()
        {
            FindObjectOfType<ResourcesManager>().AddResources(resouces);
            return base.KillCharacter();
        }

        public override string GetSpecial()
        {
            return special;
        }
    }
}