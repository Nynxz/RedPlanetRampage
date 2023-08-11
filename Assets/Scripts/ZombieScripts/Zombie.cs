using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zombie : MonoBehaviour, IDamageable {

    [SerializeField] private float startingHealth;
    [SerializeField] private RectTransform healthBarRect;

    private float currentHealth;

    protected virtual void Start() {
        currentHealth = startingHealth;
        healthBarRect.localScale.Set(1f, 1f, 1f);
    }

    public virtual void Damage(float amount) {
        currentHealth -= amount;
        UpdateHealthVisual();
        if (currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Heal(float amount) {
        if (currentHealth + amount > startingHealth) {
            currentHealth = startingHealth;
        } else {
            currentHealth += amount;
        }
        UpdateHealthVisual();
    }

    public virtual void Die() {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }

    protected void UpdateHealthVisual() {
        float normalizedHealth = (currentHealth) / (startingHealth);
        Debug.Log(normalizedHealth.ToString());
        healthBarRect.localScale = new Vector3(normalizedHealth, 1f, 1f);
    }
}
