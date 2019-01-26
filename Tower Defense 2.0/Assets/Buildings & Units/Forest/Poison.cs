using System.Collections;
using Towers.Enemies;
using UnityEngine;

namespace Towers.Units
{
    public class Poison : MonoBehaviour
    {
        int poisonLevel = 1;
        float poisonDamageMultiplayer = 1.33f;
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
                health.TakeDamage(poisonLevel * poisonDamageMultiplayer / 4f);
                yield return new WaitForSeconds(0.25f);
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