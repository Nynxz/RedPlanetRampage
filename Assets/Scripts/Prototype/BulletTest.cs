using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletTest : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private int lifeTime;
    [SerializeField] private LayerMask enemyLayerMask;

    private Vector3 lastPos;
    private float damage;
    private float headshotMultiplier = 2.5f;

    public void Setup(Vector3 forceDir, float speed, float damage) {
        SetForce(forceDir, speed);
        SetDamage(damage);
    }


    private void Awake() {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(AutoRemove());
    }

    IEnumerator AutoRemove() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void SetDamage(float damage) {
        this.damage = damage;
    }
    public void SetForce(Vector3 dir, float speed) {
        rb.AddForce(dir * speed);
    }

    // https://www.youtube.com/watch?v=Rqs81nnUlBY
    private void Update() {
        if (Physics.Raycast(lastPos, (transform.position - lastPos).normalized, out RaycastHit hit, Vector3.Distance(transform.position, lastPos), enemyLayerMask)) {
            Destroy(gameObject);

            EnemyTest enemy = hit.collider.GetComponentInParent<EnemyTest>();
            bool hitHead = hit.collider.TryGetComponent(out HeadHitbox headHitBox);
            if (enemy) {
                if(hitHead) {
                    enemy.Damage(damage * headshotMultiplier);
                    Instantiate(GameManager.Instance.UIManager.hitmarkerHeadshot, GameManager.Instance.UIManager.UICanvas.transform, false);
                    GameManager.Instance.PlayerManager.AddScore(10);
                } else {
                    enemy.Damage(damage);
                    Instantiate(GameManager.Instance.UIManager.hitmarkerRegular, GameManager.Instance.UIManager.UICanvas.transform, false);
                    GameManager.Instance.PlayerManager.AddScore(5);
                }
                
            }
        };

    }

    private void LateUpdate() {
        lastPos = transform.position;
    }

}
