using UnityEngine;

namespace Towers.Units {
    public class ButcherShopShooter : Shooter
    {
        protected override void Shoot()
        {
            base.Shoot();
            target.GetComponent<Animator>().SetTrigger("StunnedByPickup");
        }
    }
}
