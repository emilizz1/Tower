using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Towers.Enemies;

namespace Towers.Units
{
    public class Shooter : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] [Range(0.1f, 2f)] float timeBetweenAttacks = 1f;
        [SerializeField] protected float baseDamage = 50f;
        [SerializeField] float maxAttackRange = 10f;
        [SerializeField] protected ProjectileSystem projectileSystem;
        [SerializeField] protected Transform projectileSocket;
        [SerializeField] AttackType attackType;

        [Header("Animator Settings")]
        [SerializeField] RuntimeAnimatorController animatorController;
        [SerializeField] AnimatorOverrideController animatorOverrideController;
        [SerializeField] Avatar characterAvatar;

        [Header("Captule Collider")]
        [SerializeField] Vector3 colliderCenter = new Vector3(0f,1.5f,0f);
        [SerializeField] float colliderHeight = 2.5f;

        public enum AttackType
        {
            Arrows,
            Bullets,
            Magic,
            ThrowableObjects
        }

        NavMeshAgent navMeshAgent;
        Vector3 permenentPossition;
        protected EnemyAI target;
        Animator animator;
        float lastHitTime;
        protected EnemySpawner enemySpawner;

        const string ATTACK_TRIGGER = "Attacking";
        const string DEFAULT_ATTACK = "DEFAULT ATTACK";

        void Start()
        {
            AddRequiredComponents();
            enemySpawner = FindObjectOfType<EnemySpawner>();
            animator = GetComponent<Animator>();
            SetAttackAnimation();
            SetAnimatorSpeed();
        }
        
        void Update()
        {
            if (target == null)
            {
                CheckForClosestTarget();
            }
            else if (IsTargetInRange(target.gameObject) && IsTargetAlive(target.gameObject))
            {
                StartCoroutine(AttackTargetRepeatadly());
            }
            else
            {
                StopAllCoroutines();
                CheckForClosestTarget();
            }
            if(target != null)
            {
                transform.LookAt(target.transform);
            }
            CorrectPossition();
        }

        void AddRequiredComponents()
        {
            var capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
            capsuleCollider.center = colliderCenter;
            capsuleCollider.height = colliderHeight;

            var myRigidbody = gameObject.AddComponent<Rigidbody>();
            myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0.5f; // if wanted to change create [Header("Audio")] [SerializeField] float audioSourceSpatialBlend = 0.5f;

            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            animator.avatar = characterAvatar;
        }

        void SetAttackAnimation()
        {
            if (!animatorOverrideController)
            {
                Debug.Break();
                Debug.LogAssertion("please provide " + gameObject + "with animator override controller");
            }
            animator.runtimeAnimatorController = animatorOverrideController;
        }

        private void SetAnimatorSpeed()
        {
            var attackClip = animatorOverrideController[DEFAULT_ATTACK];
            while (attackClip.length / animator.speed >= timeBetweenAttacks) // if its too fast
            {
                animator.speed += 0.01f;
            }
            while (attackClip.length / animator.speed <= timeBetweenAttacks)// if its too slow
            {
                animator.speed -= 0.01f;
            }
        }

        protected virtual void CheckForClosestTarget()
        {
            EnemyAI closestTarget = null;
            float distanceToClosestTarget = 10000; //Huge number because of comparing and looking for smallest.
            foreach (EnemyAI enemy in enemySpawner.GetAllEnemies())
            {
                float distanceToEnemy = (enemy.transform.position - transform.position).magnitude;
                if (IsTargetAlive(enemy.gameObject) && distanceToEnemy < distanceToClosestTarget)
                {
                    distanceToClosestTarget = distanceToEnemy;
                    closestTarget = enemy;
                }
            }
            target = closestTarget;
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

        public AttackType GetAttackType()
        {
            return attackType;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, maxAttackRange);
        }
    }
}