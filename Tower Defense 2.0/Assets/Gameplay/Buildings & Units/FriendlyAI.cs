using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

[RequireComponent(typeof(Character))]
public class FriendlyAI : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 2f)] float timeBetweenAttacks = 1f;
    [SerializeField] float baseDamage = 50f;
    [SerializeField] float maxAttackRange = 10f;
    [SerializeField] ProjectileSystem projectileSystem;
    [SerializeField] Transform projectileSocket;

    NavMeshAgent navMeshAgent;
    Vector3 permenentPossition;
    EnemyAI target;
    Animator animator;
    Character character;
    float lastHitTime;
    EnemySpawner enemySpawner;

    const string ATTACK_TRIGGER = "Attacking";
    const string DEFAULT_ATTACK = "DEFAULT ATTACK";

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
        SetAttackAnimation();
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

    void CheckForTarget()
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

    void SetAttackAnimation()
    {
        if (!character.GetOverrideController())
        {
            Debug.Break();
            Debug.LogAssertion("please provide " + gameObject + "with animator override controller");
        }
        animator.runtimeAnimatorController = character.GetOverrideController();
    }

    bool IsTargetInRange(GameObject target)
    {
        float distanceToTarget = (target.transform.position - transform.position).magnitude;
        return distanceToTarget <= maxAttackRange;
    }

    bool IsTargetAlive(GameObject target)
    {
        float targetHealth = target.GetComponent<HealthSystem>().healthAsPercentage;
        return targetHealth > Mathf.Epsilon;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, maxAttackRange);
    }

    public void Hit()
    {
        if (target != null)
        {
            GetComponent<Church>().Play(target.transform, baseDamage);
        }
    }

    public void Shoot()
    {
        if (target != null)
        {
            if (projectileSystem != null)
            {
                projectileSystem.Shoot(target.transform ,projectileSocket);
            }
            target.GetComponent<HealthSystem>().TakeDamage(baseDamage);
        }
    }

    void CorrectPossition()
    {
        if (permenentPossition != new Vector3(0f,0f,0f) && Vector3.Distance(transform.position, permenentPossition) > 0.3f)
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
}
