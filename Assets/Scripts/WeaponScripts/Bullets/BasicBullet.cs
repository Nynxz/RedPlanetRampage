using UnityEngine;

public class BasicBullet : ProjectileBullet {
    [SerializeField] private bool doDifferent;

    public override void Setup(Vector3 forceDir, float speed, float damage) {
        base.Setup(forceDir, speed, damage);
        if (OnShootAudio) {
            GameManager.Instance.AudioManager.PlayAudioClipOnPlayer(OnShootAudio);
        }
    }

    public override void OnEnemyHit(RaycastHit raycastHit) {
        IDamageable enemy = raycastHit.transform.root.GetComponent<IDamageable>();
        bool hitHead = raycastHit.collider.TryGetComponent<HeadHitbox>(out _);

        float damageToDeal = hitHead ? damage * headshotMultiplier : damage;
        enemy.Damage(damageToDeal);

        Instantiate(hitHead ? GameManager.Instance.UIManager.gameUIVars.hitmarkerHeadshot : GameManager.Instance.UIManager.gameUIVars.hitmarkerRegular
            , GameManager.Instance.UIManager.UICanvas.transform, false);

        GameManager.Instance.PlayerManager.AddScore(10);
        Destroy(gameObject);
    }

    public override void OnGroundHit() {
        Destroy(gameObject);
    }

    public void Update() {
        if (Physics.Raycast(lastPos, (transform.position - lastPos).normalized, out RaycastHit hitInfo, Vector3.Distance(transform.position, lastPos), enemyAndGroundLayerMask)) {
            if (groundLayerMask == (groundLayerMask | (1 << hitInfo.collider.gameObject.layer))) { //something something bitwise magic
                DoGroundHit();
            } else if (enemyLayerMask == (enemyLayerMask | (1 << hitInfo.collider.gameObject.layer))) {
                DoEnemyHit(hitInfo);
            }
        }
    }
}
