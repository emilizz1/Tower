using Towers.Enemies;
using UnityEngine;

namespace Towers.Units
{
    // Deals AOE damage
    public class ChurchShooter : Shooter
    {
        [SerializeField] GameObject ps;
        [SerializeField] float radius;

        protected override void Shoot()
        {
            base.Shoot();
            if (target != null)
            {
                GameObject newPartacle = ps;
                newPartacle = Instantiate(newPartacle, target.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity, target.transform);
                DealRadialDamage(baseDamage / 2, target.transform); //AEO damage is halved
                target.GetComponent<HealthSystem>().TakeDamage(baseDamage, this);
                Destroy(newPartacle, 3f);
            }
        }

        private void DealRadialDamage(float damage, Transform target)
        {
            RaycastHit[] hits = Physics.SphereCastAll(target.position, radius, Vector3.up, radius);
            foreach (RaycastHit hit in hits)
            {
                var damageable = hit.collider.gameObject.GetComponent<HealthSystem>();
                if (damageable != null && hit.transform != target)
                {
                    damageable.TakeDamage(damage, this);
                }
            }
        }
    }
}