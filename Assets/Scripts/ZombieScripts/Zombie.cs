using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Zombie : MonoBehaviour, IDamageable {

    [SerializeField] private float startingHealth;
    [SerializeField] private RectTransform healthBarRect;
    [SerializeField] private DropTableSO drops;

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
        DropAnItem();
        Destroy(gameObject);
    }

    private void DropAnItem() {
        if (drops != null) {
            ItemSO item = drops.RandomDrop();
            if (item == null) {
                return;
            }
            item.DropItem(transform.position);
        }
    }

    protected void UpdateHealthVisual() {
        float normalizedHealth = (currentHealth) / (startingHealth);
        Debug.Log(normalizedHealth.ToString());
        healthBarRect.localScale = new Vector3(normalizedHealth, 1f, 1f);
    }
}
