using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour {

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] Image healthBar;
    [SerializeField] float deathVanishSeconds = 2f;

    float currentHealthPoints = 0;
    public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }

    // Use this for initialization
    void Start () {
        currentHealthPoints = maxHealthPoints;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateHealthBar();
	}

    void UpdateHealthBar()
    {
        if (healthBar)
        {
            healthBar.fillAmount = healthAsPercentage;
        }
    }

    public void TakeDamage(float damage)
    {
        bool characterDies = (currentHealthPoints - damage <= 0);
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
        if (characterDies)
        {
            StartCoroutine(KillCharacter());
        }
    }

    private IEnumerator KillCharacter()
    {
        //character.Kill();
        //animator.SetTrigger(Death_Trigger);
        Destroy(gameObject, deathVanishSeconds);
        yield return new WaitForSecondsRealtime(deathVanishSeconds);
        
    }
}
