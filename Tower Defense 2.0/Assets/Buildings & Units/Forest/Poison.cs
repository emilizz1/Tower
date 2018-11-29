using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers.Buildings.Forest
{
    public class Poison : MonoBehaviour
    {
        int poisonLevel = 1;
        float poisonDamageMultiplayer = 1.25f;
        HealthSystem health;

        void Start()
        {
            health = GetComponent<HealthSystem>();
            StartCoroutine(Poisoned());
        }

        IEnumerator Poisoned()
        {
            while (health.healthAsPercentage != 0)
            {
                health.TakeDamage(poisonLevel * poisonDamageMultiplayer);
                yield return new WaitForSeconds(1f);
            }
        }

        public void AddPoison()
        {
            if (poisonLevel < 3)
            {
                poisonLevel++;
            }
        }

        public int GetCurrentPoison()
        {
            return poisonLevel;
        }
    }
}