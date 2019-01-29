using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Towers.CharacterN;
using Towers.Units;
using System;

namespace Towers.Enemies
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] float maxHealthPoints = 100f;
        [SerializeField] Image healthBar;
        [SerializeField] float deathVanishSeconds = 1f;
        [SerializeField] GameObject damageNumber;

        protected string Death_Trigger = "Death";
        float currentHealthPoints = 0;
        Animator animator;
        public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }
        Character character;

        void Start()
        {
            SetHealthToMax();
            animator = GetComponent<Animator>();
            character = GetComponent<Character>();
        }

        void Update()
        {
            UpdateHealthBar();
        }

        void UpdateHealthBar()
        {
            if (healthBar)
            {
                healthBar.fillAmount = healthAsPercentage;
            }
        }

        public virtual void TakeDamage(float damage, Shooter shooter = null)
        {
            bool characterDies = (currentHealthPoints - damage <= 0);
            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
            //ShowDamageNumber(damage);
            if (characterDies)
            {
                StartCoroutine(KillCharacter());
            }
        }

        private void ShowDamageNumber(float damage)
        {
            var damageNum = Instantiate(damageNumber, healthBar.transform.position, Quaternion.identity, healthBar.transform);
            damageNum.GetComponent<Text>().text = damage.ToString();
            StartCoroutine(DamageNumberFloat(damageNum.GetComponent<Text>()));
        }

        IEnumerator DamageNumberFloat(Text damageNum)
        {
            while(damageNum.color.a > 0)
            {
                damageNum.transform.position += new Vector3(0f, 0.01f, 0f);
                damageNum.color -= new Color(0f, 0f, 0f, 0.01f);
                yield return new WaitForFixedUpdate();
            }
            Destroy(damageNum.gameObject);
        }

        public virtual IEnumerator KillCharacter()
        {
            Death();
            yield return new WaitForSecondsRealtime(deathVanishSeconds);
        }

        void DestroyColliders()
        {
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(GetComponent<EnemyAI>());
            Destroy(character);
        }

        public float GetMaxHP()
        {
            return maxHealthPoints;
        }

        public void SetHealthToMax()
        {
            currentHealthPoints = maxHealthPoints;
        }

        public void Death()
        {
            animator.SetTrigger(Death_Trigger);
            character.Kill();
            Destroy(gameObject, deathVanishSeconds);
            DestroyColliders();
        }
        
        public virtual bool ImunityToPoison() //TODO make this universal
        {
            return false;
        }
    }
}