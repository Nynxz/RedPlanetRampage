using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameManager gameManager;

    private int currentHealth;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform cameraRoot;

    [SerializeField]    [Range(0f, 1f)] private float shootCooldown;
    private float currentShoot;
    // Testing Weapons

    private bool toShoot = false;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) {
            toShoot = true;
        }
        if(currentShoot > 0) {
            currentShoot -= Time.deltaTime;
        }
    }
    private void LateUpdate() {
        if(toShoot && currentShoot <= 0) {
            Shoot();
            toShoot = false;
            currentShoot = shootCooldown;
        }
    }

    private void Shoot() {
        GameObject b = Instantiate(bullet, cameraRoot.position, cameraRoot.rotation);
        b.GetComponent<BulletTest>().SetForce(cameraRoot.forward);
    }
}
