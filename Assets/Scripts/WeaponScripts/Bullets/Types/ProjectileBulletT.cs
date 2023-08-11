using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class ProjectileBullet : Bullet {

    [SerializeField] private int maxLifeTime;

    protected Vector3 lastPos;
    private Rigidbody rb;
    protected float damage;
    protected float headshotMultiplier = 2.5f;
    protected Action<RaycastHit> DoEnemyHit;
    protected Action DoGroundHit;

    public virtual void Setup(Vector3 forceDir, float speed, float damage) {
        rb.AddForce(forceDir * speed);
        this.damage = damage;
    }

    public virtual void OnEnable() {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(AutoRemove(gameObject, maxLifeTime));

        enemyAndGroundLayerMask = groundLayerMask | enemyLayerMask;

        DoGroundHit = OnGroundHit;
        DoEnemyHit = OnEnemyHit;
    }

    public virtual void LateUpdate() {
        lastPos = transform.position;
    }

    public abstract void OnGroundHit();
    public abstract void OnEnemyHit(RaycastHit hitInfo);


    static private IEnumerator AutoRemove(GameObject g, int lifeTime) {
        yield return new WaitForSeconds(lifeTime);
        Destroy(g);
    }
}


