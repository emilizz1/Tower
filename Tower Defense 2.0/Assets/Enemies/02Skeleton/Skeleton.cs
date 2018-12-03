﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers.Enemies.Skeleton
{
    public class Skeleton : HealthSystem
    {
        [SerializeField] float layingTime = 1f;

        bool once = true;
        bool dead = false;

        string RESSURECTION_TRIGGER = "Ressurect";
        string Death_Trigger = "Death";

        public override IEnumerator KillCharacter()
        {
            if (once)
            {
                dead = true;
                Animator animator = GetComponent<Animator>();
                animator.SetTrigger(Death_Trigger);
                yield return new WaitForSecondsRealtime(layingTime);
                animator.SetTrigger(RESSURECTION_TRIGGER);
                SetHealthToMax();
                dead = false;
                once = false;
            }
            else
            {
                Death(); 
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (dead)
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
            }
        }
    }
}