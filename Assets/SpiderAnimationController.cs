using UnityEngine;

public class SpiderAnimationController : MonoBehaviour {
    public bool isChasing = false;

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    public void IsAwake() {
        isChasing = true;
    }

    public void Attack() {
        animator.SetTrigger("attack");
    }

    public void RangedAttack() {
        animator.SetTrigger("rangedattack");
    }

    public void Die() {
        animator.SetTrigger("die");
    }
}
