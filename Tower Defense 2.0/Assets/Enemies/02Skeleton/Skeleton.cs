using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers.Enemies.Skeleton
{
    public class Skeleton : HealthSystem
    {
        [SerializeField] float layingTime = 1f;

        bool once = true;

        string RESSURECTION_TRIGGER = "Ressurect";
        string Death_Trigger = "Death";

        public override IEnumerator KillCharacter()
        {
            if (once)
            {
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
    }
}