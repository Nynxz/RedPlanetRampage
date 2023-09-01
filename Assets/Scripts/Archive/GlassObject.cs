using UnityEngine;

public class GlassObject : MonoBehaviour
{
    public AudioClip shatterSound;

    private void OnCollisionEnter(Collision collision)
    {

         Debug.Log("Collision Detected");


        if (!collision.gameObject.CompareTag("Untagged"))
        {
            // Play shattering sound
            AudioSource.PlayClipAtPoint(shatterSound, transform.position);

            // Destroy the glass object
            Destroy(gameObject);
        }
    }
}