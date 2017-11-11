﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] Image healthBar;
    [SerializeField] float deathVanishSeconds = 1f;

    string Death_Trigger = "Death";
    float currentHealthPoints = 0;
    Animator animator;
    public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }
    Character character;
    
    void Start ()
    {
        currentHealthPoints = maxHealthPoints;
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
	}
	
	void Update ()
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
        character.Kill();
        animator.SetTrigger(Death_Trigger);
        Destroy(gameObject, deathVanishSeconds);
        yield return new WaitForSecondsRealtime(deathVanishSeconds);
    }
}
