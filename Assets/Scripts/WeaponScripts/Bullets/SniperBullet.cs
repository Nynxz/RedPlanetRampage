using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class SniperBullet : ProjectileBullet {

    private List<GameObject> alreadyHit;

    // Doesnt destroy itself when it hits an enemy "piercing"

    public override void OnEnable() {
        base.OnEnable();

        alreadyHit = new List<GameObject>();
        DoEnemyHit = OnEnemyHit;
    }

    public override void OnEnemyHit(RaycastHit hitInfo) {
        EnemyTest enemy = hitInfo.collider.GetComponentInParent<EnemyTest>();
        bool hitHead = hitInfo.collider.TryGetComponent<HeadHitbox>(out _);

        float damageToDeal = hitHead ? damage * headshotMultiplier : damage;
        enemy.Damage(damageToDeal);

        Instantiate(hitHead ? GameManager.Instance.UIManager.gameUIVars.hitmarkerHeadshot : GameManager.Instance.UIManager.gameUIVars.hitmarkerRegular
            , GameManager.Instance.UIManager.UICanvas.transform, false);

        GameManager.Instance.PlayerManager.AddScore(10);
    }

    public override void OnGroundHit() {
        Destroy(gameObject);
    }

    public void Update() {
        RaycastHit[] hits = Physics.RaycastAll(lastPos, (transform.position - lastPos).normalized, Vector3.Distance(transform.position, lastPos), enemyAndGroundLayerMask);
        if(hits.Length > 0 ) {
            foreach(RaycastHit hitInfo in hits) {
                if (groundLayerMask == (groundLayerMask | (1 << hitInfo.collider.gameObject.layer))) {
                    DoGroundHit();
                } else if (enemyLayerMask == (enemyLayerMask | (1 << hitInfo.collider.gameObject.layer))) {
                    if (alreadyHit.Contains(hitInfo.collider.gameObject)) {
                        continue;
                    } else {
                        alreadyHit.Add(hitInfo.collider.gameObject);
                    }
                    DoEnemyHit(hitInfo);
                }
            }
        }
    }
}
