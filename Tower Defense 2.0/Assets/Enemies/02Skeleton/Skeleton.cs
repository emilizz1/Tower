using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Towers.Units;

namespace Towers.Enemies.Skeleton
{
    public class Skeleton : HealthSystem
    {
        [SerializeField] float layingTime = 1f;

        bool once = true;

        string RESSURECTION_TRIGGER = "Ressurect";

        public override IEnumerator KillCharacter()
        {
            if (once)
            {
                if (GetComponent<Poison>())
                {
                    Destroy(GetComponent<Poison>());
                    Destroy(GetComponent<ParticleSystem>());
                }
                GetComponent<Collider>().enabled = false;
                GetComponent<NavMeshAgent>().radius = 0.000001f;
                Animator animator = GetComponent<Animator>();
                animator.SetTrigger(Death_Trigger); 
                yield return new WaitForSecondsRealtime(layingTime);
                animator.SetTrigger(RESSURECTION_TRIGGER);
                SetHealthToMax();
                once = false;
            }
            else
            {
                Death(); 
            }
        }

        void Resurected()
        {
            GetComponent<Collider>().enabled = true;
            GetComponent<NavMeshAgent>().radius = 0.25f;
        }
    }
}