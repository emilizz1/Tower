using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers.Units;

namespace Towers.Enemies
{
    public class Hero : HealthSystem
    {
        public override void TakeDamage(float damage, Shooter shooter)
        {
            if (shooter.GetShootsArrows())//TODO add magic defense
            {
                damage = damage / 2f;
            }
            base.TakeDamage(damage, shooter);
        }
    }
}
