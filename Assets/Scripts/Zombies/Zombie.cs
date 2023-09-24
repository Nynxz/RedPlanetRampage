using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Zombie : MonoBehaviour, IDamageable {
    public static event Action AZombieDiedEvent;
    public event Action ThisZombieDied;

    [SerializeField] private float startingHealth;
    [SerializeField] private RectTransform healthBarRect;
    [SerializeField] private DropTableSO drops;
    [SerializeField] protected ZombieState state;
    [SerializeField] private bool stayAsleep;
    [SerializeField] private bool drawGizmos = false;
    [SerializeField] protected float actionCooldown;

    [SerializeField] protected ZombieVariables zombieVariables;

    protected float currentCooldown;

    private float currentHealth;
    protected NavMeshAgent navAgent;
    protected float distanceToPlayer;
    protected enum ZombieState {
        Asleep,
        Wandering,
        Chasing,
        Attacking
    }

    protected virtual void Start() {
        currentHealth = startingHealth;
        healthBarRect.localScale.Set(1f, 1f, 1f);
        if (stayAsleep) {
            state = ZombieState.Asleep;
        }
        currentCooldown = 0;

        navAgent = GetComponent<NavMeshAgent>();
    }


    protected void Update() {
        if (currentCooldown >= actionCooldown) {
            currentCooldown = 0;
        }
        currentCooldown += Time.deltaTime;

        if (zombieVariables) {
            distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.PlayerManager.PlayerPosition);
            switch (state) {
                case ZombieState.Asleep: DoAsleep(); break;
                case ZombieState.Wandering: DoWandering(); break;
                case ZombieState.Chasing: DoChasing(); break;
                case ZombieState.Attacking: DoAttacking(); break;
                default: break;
            }
        }
    }

    protected virtual void DoAsleep() {
        if (distanceToPlayer < zombieVariables.awakeRange && !stayAsleep) {
            ZombieAnimationController animController = GetComponentInChildren<ZombieAnimationController>();
            if (animController != null && animController.isStanding) {
                state = ZombieState.Wandering;
            } else if (animController == null) {
                state = ZombieState.Wandering;
            }
        }
    }

    protected virtual void DoWandering() {
        if (distanceToPlayer < zombieVariables.chaseRange) {
            state = ZombieState.Chasing;
        } else if (distanceToPlayer > zombieVariables.awakeRange) {
            state = ZombieState.Asleep;
        }
    }
    protected virtual void SetDestinationToPlayer() {
        navAgent.SetDestination(GameManager.Instance.PlayerManager.PlayerPosition);
    }

    protected virtual void DoChasing() {
        SetDestinationToPlayer();
        if (distanceToPlayer < zombieVariables.attackRange) {
            state = ZombieState.Attacking;
            navAgent.enabled = false;
        } else if (distanceToPlayer > zombieVariables.chaseRange) {
            state = ZombieState.Wandering;
        }
    }

    protected virtual void DoAttacking() {
        if (distanceToPlayer > zombieVariables.attackRange) {
            navAgent.enabled = true;

            state = ZombieState.Chasing;
        } else if (currentCooldown >= actionCooldown) {
            GameManager.Instance.PlayerManager.Player.Damage(zombieVariables.attackDamage);
        }
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
        ThisZombieDied?.Invoke();
        AZombieDiedEvent?.Invoke();
        DropAnItem();
        Destroy(gameObject);
    }

    private void DropAnItem() {
        if (drops != null) {
            Item item = drops.RandomDrop();
            if (item == null) {
                return;
            }
            item.DropItem(transform.position);
        }
    }

    protected void UpdateHealthVisual() {
        float normalizedHealth = (currentHealth) / (startingHealth);
        //Debug.Log(normalizedHealth.ToString());
        healthBarRect.localScale = new Vector3(normalizedHealth, 1f, 1f);
    }

    protected void OnDrawGizmos() {
        if (!drawGizmos) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, zombieVariables.awakeRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, zombieVariables.chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, zombieVariables.attackRange);

    }
}
