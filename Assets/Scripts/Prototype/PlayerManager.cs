using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private LayerMask interactMask;

    [SerializeField]    [Range(0f, 5f)] private float interactRange;


    private StarterAssetsInputs _input;


    void Start()
    {
        gameManager = GameManager.Instance;
        _input = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (GameManager.Instance.InputManager.interactInput) {
            RaycastHit hit;
            GameManager.Instance.InputManager.SetInteract(false);
            if (Physics.Raycast(cameraRoot.position, cameraRoot.forward, out hit, interactRange, interactMask)) {
                Debug.Log("Got Hit");

                if (hit.collider.TryGetComponent(out Interactable interactable)) {
                    interactable.interact(gameObject);
                }
            }
        }
    }

    private void OnDrawGizmos() {

        // Interaction Probe
        Gizmos.color = Color.green;
        Gizmos.DrawLine(cameraRoot.position, cameraRoot.position + cameraRoot.forward * interactRange);
    }
}
