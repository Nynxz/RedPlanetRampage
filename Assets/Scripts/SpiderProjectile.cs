using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderProjectile : MonoBehaviour {
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float bulletForce;

    private SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            GameManager.Instance.PlayerManager.Player.Damage(6);
            Destroy(gameObject);
        } else if (!collision.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
        }
    }

}
