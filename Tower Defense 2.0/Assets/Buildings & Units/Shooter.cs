using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Towers.CharacterN;
using Towers.Enemies;
using System;

namespace Towers.Units
{
    [RequireComponent(typeof(Character))]
    public class Shooter : MonoBehaviour
    {
        [SerializeField] [Range(0.1f, 2f)] float timeBetweenAttacks = 1f;
        [SerializeField] protected float baseDamage = 50f;
        [SerializeField] float maxAttackRange = 10f;
        [SerializeField] protected ProjectileSystem projectileSystem;
        [SerializeField] protected Transform projectileSocket;
        [SerializeField] bool ShootsArrows;

        NavMeshAgent navMeshAgent;
        Vector3 permenentPossition;
        protected EnemyAI target;
        Animator animator;
        Character character;
        float lastHitTime;
        protected EnemySpawner enemySpawner;

        const string ATTACK_TRIGGER = "Attacking";
        const string DEFAULT_ATTACK = "DEFAULT ATTACK";

        void Start()
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
            animator = GetComponent<Animator>();
            character = GetComponent<Character>();
            SetAttackAnimation();
            SetAnimatorSpeed();
        }
        
        void Update()
        {
            if (target == null)
            {
                CheckForTarget();
            }
            else if (IsTargetInRange(target.gameObject) && IsTargetAlive(target.gameObject))
            {
                StartCoroutine(AttackTargetRepeatadly());
            }
            else
            {
                StopAllCoroutines();
                CheckForTarget();
            }
            CorrectPossition();
        }

        void SetAttackAnimation()
        {
            if (!character.GetOverrideController())
            {
                Debug.Break();
                Debug.LogAssertion("please provide " + gameObject + "with animator override controller");
            }
            animator.runtimeAnimatorController = character.GetOverrideController();
        }

        private void SetAnimatorSpeed()
        {
            var attackClip = character.GetOverrideController()[DEFAULT_ATTACK];
            while (attackClip.length / animator.speed >= timeBetweenAttacks) // if its too fast
            {
                animator.speed += 0.01f;
            }
            while (attackClip.length / animator.speed <= timeBetweenAttacks)// if its too slow
            {
                animator.speed -= 0.01f;
            }
        }

        protected virtual void CheckForTarget()
        {
            foreach (EnemyAI enemy in enemySpawner.GetAllEnemies())
            {
                if (IsTargetAlive(enemy.gameObject) && IsTargetInRange(enemy.gameObject))
                {
                    target = enemy;
                    return;
                }
            }
        }

        IEnumerator AttackTargetRepeatadly()
        {
            while (target != null && IsTargetAlive(target.gameObject) && IsTargetInRange(target.gameObject))
            {
                bool isTimeToHitAgain = Time.time - lastHitTime > timeBetweenAttacks;
                if (isTimeToHitAgain)
                {
                    AttackTargetOnce();
                    lastHitTime = Time.time;
                }
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
        }

        private void AttackTargetOnce()
        {
            transform.LookAt(target.transform);
            animator.SetTrigger(ATTACK_TRIGGER);
            SetAttackAnimation();
        }

        protected bool IsTargetInRange(GameObject target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= maxAttackRange;
        }

        protected bool IsTargetAlive(GameObject target)
        {
            float targetHealth = target.GetComponent<HealthSystem>().healthAsPercentage;
            return targetHealth > Mathf.Epsilon;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, maxAttackRange);
        }

        protected virtual void Shoot()
        {
            if (target != null)
            {
                if (projectileSystem != null)
                {
                    projectileSystem.Shoot(target.transform, projectileSocket);
                }
                target.GetComponent<HealthSystem>().TakeDamage(baseDamage, this);
            }
        }

        void CorrectPossition()
        {
            if (permenentPossition != new Vector3(0f, 0f, 0f) && Vector3.Distance(transform.position, permenentPossition) > 0.3f)
            {
                transform.position = permenentPossition;
            }
        }

        public void SetPossition()
        {
            AddNavMeshAgent();
            permenentPossition = transform.position;
        }

        void AddNavMeshAgent()
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            navMeshAgent.updateRotation = false;
            navMeshAgent.updatePosition = true;
            navMeshAgent.stoppingDistance = 0.5f;
            navMeshAgent.speed = 1f;
            navMeshAgent.autoBraking = false;
        }

        public void GiveStats(out float attack, out float speed, out float range)
        {
            attack = baseDamage;
            speed = timeBetweenAttacks;
            range = maxAttackRange;
        }

        public EnemyAI GetTarget()
        {
            return target;
        }

        public float GetRange()
        {
            return maxAttackRange;
        }

        public bool GetShootsArrows()
        {
            return ShootsArrows;
        }
    }
}