using UnityEngine;


// Used as a Gizmo Script... Pretty useless
public class PlayerSpawn : MonoBehaviour {

    protected void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
