using UnityEngine;

namespace Towers.Units
{
    public class Forest : Shooter
    {
        [SerializeField] GameObject[] poisonPS;

        float lastTimeShot = 0;

        protected override void Shoot()
        {
            base.Shoot();
            print(Time.time - lastTimeShot + "  " + gameObject.name);
            lastTimeShot = Time.time;
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