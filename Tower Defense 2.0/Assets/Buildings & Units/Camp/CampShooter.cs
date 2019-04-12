using System.Collections.Generic;
using Towers.Enemies;

namespace Towers.Units
{
    //Shoots two enemies at the same time
    public class CampShooter : Shooter
    {
        List<EnemyAI> targets = new List<EnemyAI>();
        EnemyAI lastTarget;

        protected override void CheckForClosestTarget()
        {
            targets = new List<EnemyAI>();
            foreach (EnemyAI enemy in enemySpawner.GetAllEnemies())
            {
                target = enemy; // for shooter class to have a target
                if (IsTargetAlive(enemy.gameObject) && IsTargetInRange(enemy.gameObject))
                {
                    targets.Add(enemy); // To gather all enemies
                    lastTarget = enemy; // incase it shoots and all targets out of range, to do last shot
                }
                if(targets.Count > 1)
                {
                    return;
                }
            }
        }

        protected override void Shoot()
        {
            CheckForClosestTarget();
            if(targets.Count == 0)
            {
                targets.Add(lastTarget);
            }
            if (targets.Count > 0 && projectileSystem != null)
            {
                foreach (EnemyAI enemy in targets)
                {
                    if (enemy != null)
                    {
                        projectileSystem.Shoot(enemy.transform, projectileSocket);
                        enemy.GetComponent<HealthSystem>().TakeDamage(baseDamage, this);
                    }
                }
            }
        }
    }
}