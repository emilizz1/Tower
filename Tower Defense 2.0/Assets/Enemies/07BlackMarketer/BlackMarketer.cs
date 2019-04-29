using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Towers.Resources;

namespace Towers.Enemies.BlackMarketer
{
    public class BlackMarketer : HealthSystem
    {
        [SerializeField] string special;
        [SerializeField] Resource[] resouces;
        [SerializeField] ParticleSystem particles;

        public override IEnumerator KillCharacter()
        {
            particles.Play();
            ResourcesManager resourcesManager = FindObjectOfType<ResourcesManager>();
            resourcesManager.AddResources(resouces, resourcesManager.GetComponentInChildren<Image>().transform);
            return base.KillCharacter();
        }

        public override string GetSpecial()
        {
            return special;
        }
    }
}