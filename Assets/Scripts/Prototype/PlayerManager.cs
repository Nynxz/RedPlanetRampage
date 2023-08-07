using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameManager gameManager;

    private int currentHealth;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private LayerMask interactMask;

    [SerializeField]    [Range(0f, 5f)] private float interactRange;




    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.E)) {
            if (Physics.Raycast(cameraRoot.position, cameraRoot.forward, out hit, interactRange, interactMask)) {
                Debug.Log("Got Hit");
                
                if (hit.collider.TryGetComponent(out Interactable interactable)) {
                    interactable.interact(gameObject);
                }
            }
        }
/*        if (Input.GetKeyDown(KeyCode.V)) {
            if (Time.timeScale == 1f) {
                Time.timeScale = 0.1f;
            } else {
                Time.timeScale = 1f;
            }
        }*/
    }



    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(cameraRoot.position, cameraRoot.position + cameraRoot.forward * interactRange);
    }
}
