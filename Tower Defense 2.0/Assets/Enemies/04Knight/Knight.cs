using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers.Units;

namespace Towers.Enemies
{
    public class Knight : HealthSystem
    {
        public override void TakeDamage(float damage, Shooter shooter)
        {
            if (shooter.GetAttackType() == Shooter.AttackType.Arrows)
            {
                damage = damage / 2f;
            }
            base.TakeDamage(damage, shooter);
        }
    }
}
