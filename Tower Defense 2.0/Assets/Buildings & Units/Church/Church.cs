using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers.Buildings.Church
{
    public class Church : MonoBehaviour
    {

        [SerializeField] GameObject ps;
        [SerializeField] float radius;

        public void Play(Transform target, float baseDamage)
        {
            GameObject newPartacle = ps;
            newPartacle = Instantiate(newPartacle, target.position + new Vector3(0f, 1f, 0f), Quaternion.identity, target);
            DealRadialDamage(baseDamage, target);
            Destroy(newPartacle, 3f);
        }

        private void DealRadialDamage(float damage, Transform target)
        {
            RaycastHit[] hits = Physics.SphereCastAll(target.position, radius, Vector3.up, radius);

            foreach (RaycastHit hit in hits)
            {
                var damageable = hit.collider.gameObject.GetComponent<HealthSystem>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
    }
}