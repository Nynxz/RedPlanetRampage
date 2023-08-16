using UnityEngine;

public abstract class Item : MonoBehaviour {
    protected abstract bool TryOnPickup(Player player);
    [SerializeField] private AudioClip onPickupSound;

    protected void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.TryGetComponent(out Player player)) {
            if (TryOnPickup(player)) {
                PlayPickupSound();
                Destroy(gameObject);
            }
        }
    }
    public void PlayPickupSound() {
        if (onPickupSound != null) {
            GameManager.Instance.AudioManager.PlayAudioClipOnPlayer(onPickupSound);
        }
    }

    public void DropItem(Vector3 pos) {
        Instantiate(gameObject, pos, Quaternion.identity);
    }
}
