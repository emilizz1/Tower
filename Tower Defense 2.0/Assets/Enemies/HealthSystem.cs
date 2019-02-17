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
        [SerializeField] Image redHealthBar;
        [SerializeField] Image whiteHealthBar;
        [SerializeField] float deathVanishSeconds = 1f;

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

        public virtual void TakeDamage(float damage, Shooter shooter = null)
        {
            bool characterDies = (currentHealthPoints - damage <= 0);
            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
            redHealthBar.fillAmount = healthAsPercentage;
            StartCoroutine(WhiteHealthRemoval());
            if (characterDies)
            {
                StopCoroutine(WhiteHealthRemoval());
                StartCoroutine(WhiteHealthRemoval(3f));
                StartCoroutine(KillCharacter());
            }
        }

        IEnumerator WhiteHealthRemoval( float speed = 1f)
        {
            while (whiteHealthBar.fillAmount > redHealthBar.fillAmount)
            {
                whiteHealthBar.fillAmount -= 0.001f * speed * (5 - maxHealthPoints / 100);
                yield return new WaitForFixedUpdate();
            }
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
            redHealthBar.fillAmount = 1f;
            whiteHealthBar.fillAmount = 1f;
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

        public virtual string GetSpecial()
        {
            return "";
        }
    }
}