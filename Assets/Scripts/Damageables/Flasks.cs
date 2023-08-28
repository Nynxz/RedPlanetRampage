using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasks : MonoBehaviour, IDamageable {
    [SerializeField] private AudioClip onBreakSound;

    public void Damage(float _) {
        Die();
    }

    public void Die() {
        GameManager.Instance.AudioManager.PlayAudioClipAtLocation(onBreakSound, transform.position);
        Destroy(gameObject);
    }

    public void Heal(float amount) {
        // Not Needed
    }
}
