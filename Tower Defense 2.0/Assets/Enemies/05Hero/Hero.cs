using UnityEngine;
using Towers.Units;

namespace Towers.Enemies.Hero
{
    public class Hero : HealthSystem
    {
        [SerializeField] string special;

        public override void TakeDamage(float damage, Shooter shooter)
        {
            if (shooter.GetAttackType() == Shooter.AttackType.Arrows && shooter.GetAttackType() == Shooter.AttackType.Magic)
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
