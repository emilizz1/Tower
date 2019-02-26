using UnityEngine;
using Towers.Units;

namespace Towers.Enemies.Knight
{
    public class Knight : HealthSystem
    {
        [SerializeField] string special;

        public override void TakeDamage(float damage, Shooter shooter)
        {
            print("shooter: " + shooter);
            if (shooter.GetAttackType() == Shooter.AttackType.Arrows)
            {
                damage = damage / 2f;
            }
            base.TakeDamage(damage, shooter);
        }

        public override string GetSpecial()
        {
            return special;
        }
    }
}
