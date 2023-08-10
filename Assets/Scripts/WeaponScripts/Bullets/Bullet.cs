using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected delegate bool OnEnemyHitDelegate(RaycastHit hit);

    [SerializeField] private int maxLifeTime;
    [SerializeField] protected LayerMask groundLayerMask;
    [SerializeField] protected LayerMask enemyLayerMask;

    protected LayerMask enemyAndGroundLayerMask;
    protected Vector3 lastPos;

    private Rigidbody rb;
    protected float damage;
    protected float headshotMultiplier = 2.5f;

    protected Action<RaycastHit> DoEnemyHit;
    protected Action DoGroundHit;

    public virtual void OnEnable() {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(AutoRemove(gameObject, maxLifeTime));
        enemyAndGroundLayerMask = groundLayerMask | enemyLayerMask;
        DoGroundHit = BaseGroundHit;
        DoEnemyHit = BaseEnemyHit;
    }

    public void Setup(Vector3 forceDir, float speed, float damage) {
        rb.AddForce(forceDir *  speed);
        this.damage = damage;
    }

    public virtual void Update() {
        if(Physics.Raycast(lastPos, (transform.position- lastPos).normalized, out RaycastHit hitInfo, Vector3.Distance(transform.position, lastPos), enemyAndGroundLayerMask)) {
            if(groundLayerMask == (groundLayerMask | (1 << hitInfo.collider.gameObject.layer))) {
                DoGroundHit();
            } else if(enemyLayerMask == (enemyLayerMask | (1 << hitInfo.collider.gameObject.layer))) {
                DoEnemyHit(hitInfo);
            }
        }
    }

    public virtual void LateUpdate() {
        lastPos = transform.position;
    }

    private void BaseGroundHit() {
        Destroy(gameObject);
    }

    private void BaseEnemyHit(RaycastHit raycastHit) {
        Debug.Log("Base Enemy Hit");
        EnemyTest enemy = raycastHit.collider.GetComponentInParent<EnemyTest>();
        bool hitHead = raycastHit.collider.TryGetComponent<HeadHitbox>(out _);

        float damageToDeal = hitHead ? damage * headshotMultiplier : damage;
        Debug.Log($"{damageToDeal} dmg");
        enemy.Damage(damageToDeal);

        Instantiate(hitHead ? GameManager.Instance.UIManager.gameUIVars.hitmarkerHeadshot : GameManager.Instance.UIManager.gameUIVars.hitmarkerRegular
            , GameManager.Instance.UIManager.UICanvas.transform, false);

        GameManager.Instance.PlayerManager.AddScore(10);
        Destroy(gameObject);
    }


    static private IEnumerator AutoRemove(GameObject g, int lifeTime) {
        yield return new WaitForSeconds(lifeTime);
        Destroy(g);
    }

}
