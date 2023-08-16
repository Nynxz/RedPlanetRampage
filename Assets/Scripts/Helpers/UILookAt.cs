using UnityEngine;

// Rotates the gameobject to face the camera, used for healthbars currently
public class UILookAt : MonoBehaviour {
    //https://www.youtube.com/watch?v=ccqiNWsYJnI
    protected void LateUpdate() {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
