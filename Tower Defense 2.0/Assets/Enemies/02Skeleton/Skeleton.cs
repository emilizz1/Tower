using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Towers.Enemies.Skeleton
{
    public class Skeleton : HealthSystem
    {
        [SerializeField] float layingTime = 1f;

        bool once = true;
        bool readyToDie = false;

        string RESSURECTION_TRIGGER = "Ressurect";

        public override IEnumerator KillCharacter()
        {
            if (once)
            {
                once = false;
                GetComponent<Collider>().enabled = false;
                GetComponent<NavMeshAgent>().radius = 0.000001f;
                Animator animator = GetComponent<Animator>();
                animator.SetTrigger(Death_Trigger); 
                yield return new WaitForSecondsRealtime(layingTime);
                animator.SetTrigger(RESSURECTION_TRIGGER);
                SetHealthToMax();
                readyToDie = true;
            }
            else if(readyToDie)
            {
                Death(); 
            }
        }

        void Resurected()
        {
            if (GetComponent<Collider>())
            {
                GetComponent<Collider>().enabled = true;
            }
            GetComponent<NavMeshAgent>().radius = 0.25f;
        }

        public override bool ImunityToPoison()
        {
            return true;
        }
    }
}