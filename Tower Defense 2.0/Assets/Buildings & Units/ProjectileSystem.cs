using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers.Units
{
    [CreateAssetMenu(menuName = ("Tower defense/ProjectileSystem"))]
    public class ProjectileSystem : ScriptableObject
    {
        [SerializeField] Projectile projectile;
        [SerializeField] float projectileSpeed;
        Vector3 aimOffset = new Vector3(0f, 1f, 0f);

        public void Shoot(Transform target, Transform projectileSocket)
        {
            Projectile newProjectile = projectile;
            newProjectile = Instantiate(newProjectile, projectileSocket.position, Quaternion.identity);
            Vector3 unitVectorToEnemy = (target.transform.position + aimOffset - projectileSocket.position).normalized;
            newProjectile.transform.LookAt(target.transform);
            newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToEnemy * projectileSpeed;
        }
    }
}