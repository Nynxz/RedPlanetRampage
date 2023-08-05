using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private int lifeTime;
    [SerializeField] private LayerMask enemyLayerMask;

    private Vector3 lastPos;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(AutoRemove());
    }

    IEnumerator AutoRemove() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    public void SetForce(Vector3 dir) {
        rb.AddForce(dir * speed);
    }

    // https://www.youtube.com/watch?v=Rqs81nnUlBY
    private void Update() {
        if(Physics.Linecast(lastPos, transform.position, enemyLayerMask)) {
            Destroy(gameObject);
            Debug.Log("Hit Enemy");
            GameManager.Instance.AddScore(10);
        }
    }

    private void LateUpdate() {
        lastPos = transform.position;
    }

}
