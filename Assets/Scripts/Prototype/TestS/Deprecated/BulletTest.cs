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

    [SerializeField] private bool destroyOnEnemyHit = false;
    private List<GameObject> alreadyHit;

    public void Setup(Vector3 forceDir, float speed, float damage) {
        SetForce(forceDir, speed);
        SetDamage(damage);
    }


    private void Awake() {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(AutoRemove());
        alreadyHit = new List<GameObject>();

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
        // TODO: Issue, what if multiple enemies are in the raycast line
        if (!destroyOnEnemyHit) {
            RaycastHit[] hits = Physics.RaycastAll(lastPos, (transform.position - lastPos).normalized, Vector3.Distance(transform.position, lastPos), enemyLayerMask);
            Debug.Log(hits.Length);
            if (hits.Length > 0) {
                foreach (RaycastHit hit in hits) {

                    EnemyTest enemy = hit.collider.GetComponentInParent<EnemyTest>();
                    bool hitHead = hit.collider.TryGetComponent(out HeadHitbox headHitBox);

                    if (enemy) {

                        if (alreadyHit.Contains(enemy.gameObject)) {
                            continue;
                        } else {
                            alreadyHit.Add(enemy.gameObject);
                        }
                        Debug.Log("Hit Enemy");
                        if (hitHead) {
                            enemy.Damage(damage * headshotMultiplier);
                            Instantiate(GameManager.Instance.UIManager.gameUIVars.hitmarkerHeadshot, GameManager.Instance.UIManager.UICanvas.transform, false);
                            GameManager.Instance.PlayerManager.AddScore(10);
                        } else {
                            enemy.Damage(damage);
                            Instantiate(GameManager.Instance.UIManager.gameUIVars.hitmarkerRegular, GameManager.Instance.UIManager.UICanvas.transform, false);
                            GameManager.Instance.PlayerManager.AddScore(5);
                        }

                    }
                }
                Debug.Log(alreadyHit.Count);

            }
        } 
        else if (Physics.Raycast(lastPos, (transform.position - lastPos).normalized, out RaycastHit hit, Vector3.Distance(transform.position, lastPos), enemyLayerMask)) {
            Destroy(gameObject);

            EnemyTest enemy = hit.collider.GetComponentInParent<EnemyTest>();
            bool hitHead = hit.collider.TryGetComponent(out HeadHitbox headHitBox);
            if (enemy) {
                if (hitHead) {
                    enemy.Damage(damage * headshotMultiplier);
                    Instantiate(GameManager.Instance.UIManager.gameUIVars.hitmarkerHeadshot, GameManager.Instance.UIManager.UICanvas.transform, false);
                    GameManager.Instance.PlayerManager.AddScore(10);
                } else {
                    enemy.Damage(damage);
                    Instantiate(GameManager.Instance.UIManager.gameUIVars.hitmarkerRegular, GameManager.Instance.UIManager.UICanvas.transform, false);
                    GameManager.Instance.PlayerManager.AddScore(5);
                }
            }

        };

    }

    private void LateUpdate() {
        lastPos = transform.position;
    }
}
