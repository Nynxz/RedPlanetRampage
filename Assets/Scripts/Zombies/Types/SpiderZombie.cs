using UnityEngine;

public class SpiderZombie : Zombie {
    [SerializeField] private Transform rangedAttackPoint;
    [SerializeField] private float rangedAttackRange;
    [SerializeField] private GameObject rangedAttackPrefab;

    protected override void DoChasing() {
        SetDestinationToPlayer();
        if (distanceToPlayer < rangedAttackRange) {
            state = ZombieState.Attacking;
            navAgent.enabled = false;
        } else if (distanceToPlayer > zombieVariables.chaseRange) {
            state = ZombieState.Wandering;
        }
    }

    protected override void DoAttacking() {
        //https://discussions.unity.com/t/how-to-know-if-an-object-is-looking-at-an-other/144150
        SpiderAnimationController spiderAnimationController = GetComponent<SpiderAnimationController>();
        Vector3 dirFromAtoB = (GameManager.Instance.PlayerManager.Player.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirFromAtoB), Time.deltaTime * 5);
        if (distanceToPlayer < zombieVariables.attackRange) { //If player is too close, melee
            if(currentCooldown >= actionCooldown) {
                spiderAnimationController.Attack();
                GameManager.Instance.PlayerManager.Player.Damage(zombieVariables.attackDamage);
            }
        }
        else if (distanceToPlayer < rangedAttackRange) { // If we are within ranged distance

            float dotProd = Vector3.Dot(dirFromAtoB, transform.forward);

            if (currentCooldown >= actionCooldown && dotProd > 0.9 ) { // If we can attack and facing the player

                spiderAnimationController.RangedAttack();
                Instantiate(rangedAttackPrefab, rangedAttackPoint.position, transform.rotation);
            } 
        } else {
            state = ZombieState.Chasing;
            navAgent.enabled = true;
        }
    }
}
