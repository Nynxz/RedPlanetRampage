using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallZombie : Zombie {
    [SerializeField] private float zombieExplosionDamage = 8f;
    [SerializeField] private float zombieSlowTime = 5f;
    protected override void DoAttacking() {
        if (distanceToPlayer > zombieVariables.attackRange) {
            navAgent.enabled = true;

            state = ZombieState.Chasing;
        } else if (currentCooldown >= actionCooldown) {
            GameManager.Instance.PlayerManager.Player.Damage(zombieExplosionDamage);
            GameManager.Instance.PlayerManager.Player.AddSlowDebuff(zombieSlowTime);
            Destroy(gameObject);
        }
    }
}
