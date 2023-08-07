using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private RectTransform healthBarRect;
    
    private float currentHealth;
    private NavMeshAgent agent;
    
    void Start()
    {
        currentHealth = startingHealth;
        healthBarRect.localScale.Set(1f, 1f, 1f);
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(GameManager.Instance.playerTransform.position);
    }

    private void Update() {
        agent.SetDestination(GameManager.Instance.playerTransform.position);
    }

    public void Damage(float amount) {
        Debug.Log("Damaging Enemy");
        currentHealth -= amount;
        UpdateHealthVisual();
        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }

    private void UpdateHealthVisual() {
        float normalizedHealth = (currentHealth) / (startingHealth);
        Debug.Log(normalizedHealth.ToString());
        healthBarRect.localScale = new Vector3(normalizedHealth, 1f, 1f);
    }

}
