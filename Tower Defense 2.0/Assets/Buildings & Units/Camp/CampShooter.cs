using System.Collections.Generic;
using Towers.Enemies;

namespace Towers.Units
{
    //Shoots two enemies at the same time
    public class CampShooter : Shooter
    {
        List<EnemyAI> targets = new List<EnemyAI>();

        protected override void CheckForTarget()
        {
            targets = new List<EnemyAI>();
            foreach (EnemyAI enemy in enemySpawner.GetAllEnemies())
            {
                if (IsTargetAlive(enemy.gameObject) && IsTargetInRange(enemy.gameObject))
                {
                    targets.Add(enemy);
                    target = enemy;
                }
                if(targets.Count > 1)
                {
                    return;
                }
            }
        }

        protected override void Shoot()
        {
            CheckForTarget();
            if (targets.Count > 0 && projectileSystem != null)
            {
                foreach (EnemyAI enemy in targets)
                {
                    projectileSystem.Shoot(enemy.transform, projectileSocket);
                    enemy.GetComponent<HealthSystem>().TakeDamage(baseDamage);
                }
            }
        }
    }
}