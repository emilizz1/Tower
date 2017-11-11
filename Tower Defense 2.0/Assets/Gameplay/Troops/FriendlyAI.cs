using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(HealthSystem))]
public class FriendlyAI : MonoBehaviour
{
    [SerializeField] float baseDamage = 50f;
    [SerializeField] float maxAttackRange = 10f;
    [SerializeField] AnimationClip attackAnimation;

    List<EnemyAI> targetList = new List<EnemyAI>(); 
    EnemyAI target;
    Animator animator;
    Character character;
    float lastHitTime;

    const string ATTACK_TRIGGER = "Attacking";
    const string DEFAULT_ATTACK = "DEFAULT ATTACK";

    void Start()
    {
        var sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = maxAttackRange;
        sphereCollider.isTrigger = true;
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
        else if (IsTargetInRange() && IsTargetAlive())
        {
            AttackTarget();
        }
        else if (!IsTargetAlive() || !IsTargetInRange())
        {
            StopAllCoroutines();
            targetList.Remove(target);
            target = null;
        }
    }

    void CheckForTarget()
    {
        if(targetList.Count != 0)
        {
            target = targetList.First();
        }
    }

    void ClearTheList()
    {
        if (targetList.Count >= 2)
        {
            List<EnemyAI> enemyList = targetList;
            targetList = new List<EnemyAI>();
            foreach (EnemyAI enemy in enemyList)
            {
                if(enemy == null)
                {
                    return;
                }
                if (enemy != null || enemy.GetComponent<HealthSystem>().healthAsPercentage > Mathf.Epsilon || (enemy.transform.position - transform.position).magnitude < maxAttackRange)
                {
                    targetList.Add(enemy);
                }
            }
        }
    }

    public void AttackTarget()
    {
        StartCoroutine(AttackTargetRepeatadly());
    }

    IEnumerator AttackTargetRepeatadly()
    {
        while (IsTargetAlive() && IsTargetInRange())
        {
            float timeToWait = attackAnimation.length * character.GetAnimSpeedMultiplier();
            bool isTimeToHitAgain = Time.time - lastHitTime > timeToWait;
            if (isTimeToHitAgain)
            {
                AttackTargetOnce();
                lastHitTime = Time.time;
            }
            yield return new WaitForSeconds(timeToWait);
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
        var animatorOverrideController = character.GetOverrideController();
        animatorOverrideController[DEFAULT_ATTACK] = attackAnimation;
        animator.runtimeAnimatorController = animatorOverrideController;
    }

    public void StopAttacking()
    {
        animator.StopPlayback();
        StopAllCoroutines();
    }

    bool IsTargetInRange()
    {
        float distanceToTarget = (target.transform.position - transform.position).magnitude;
        return distanceToTarget <= maxAttackRange;
    }

    bool IsTargetAlive()
    {
        float targetHealth = target.GetComponent<HealthSystem>().healthAsPercentage;
        return targetHealth > Mathf.Epsilon;
    }

    public float GetMaxAttackRange()
    {
        return maxAttackRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyAI tempEnemy;
        if (other.GetComponent<EnemyAI>())
        {
            tempEnemy = other.GetComponent<EnemyAI>();
            foreach(EnemyAI enemy in targetList)
            {
                if(enemy == tempEnemy)
                {
                    return;
                }
            }
            targetList.Add(other.GetComponent<EnemyAI>());
            ClearTheList();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, maxAttackRange);
    }

    public void Shoot()
    {
        if (target != null)
        {
            target.GetComponent<HealthSystem>().TakeDamage(baseDamage);
        }
    }
}
