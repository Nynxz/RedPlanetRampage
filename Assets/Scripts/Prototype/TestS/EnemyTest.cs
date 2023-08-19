using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour {
    [SerializeField] private float startingHealth;
    [SerializeField] private RectTransform healthBarRect;

    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    private float currentAttackCooldown = 0;


    private float currentHealth;
    private NavMeshAgent agent;

    void Start() {
        currentHealth = startingHealth;
        healthBarRect.localScale.Set(1f, 1f, 1f);
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(GameManager.Instance.PlayerManager.PlayerPosition);
    }

    private void Update() {
        agent.SetDestination(GameManager.Instance.PlayerManager.PlayerPosition);
        if (Vector3.Distance(transform.position, GameManager.Instance.PlayerManager.PlayerPosition) <= attackRange) {
            Attack();
        }

        if (currentAttackCooldown <= attackCooldown) {
            currentAttackCooldown += Time.deltaTime;
        }
    }

    public void Attack() {
        if (currentAttackCooldown >= attackCooldown) {
            currentAttackCooldown = 0;
            GameManager.Instance.PlayerManager.Player.Damage(attackDamage);
            Debug.Log("Attacking player for " + attackDamage + " damage");
        }
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

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
