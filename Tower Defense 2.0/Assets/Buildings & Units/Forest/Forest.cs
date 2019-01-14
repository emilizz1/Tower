using UnityEngine;

namespace Towers.Units
{
    public class Forest : Shooter
    {
        [SerializeField] GameObject[] poisonPS;
        
        protected override void Shoot()
        {
            base.Shoot();
            if (target != null)
            {
                Poison poison;
                if (target.GetComponent<Poison>())
                {
                    poison = target.GetComponent<Poison>();
                    poison.AddPoison();
                }
                else
                {
                    target.gameObject.AddComponent<Poison>();
                }
                Instantiate(poisonPS[target.GetComponent<Poison>().GetCurrentPoison() - 1], target.transform);
                target = null;
            }
        }
    }
}