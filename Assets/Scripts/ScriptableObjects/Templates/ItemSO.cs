using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "Data/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;
    public AudioClip onPickupSound;

    public void DropItem(Vector3 pos) {
        Instantiate(itemPrefab, pos, Quaternion.identity)
            .GetComponent<Item>()
            .Init(this);
    }

    public void PlayPickupSound() {
        if(onPickupSound != null) {
            GameManager.Instance.AudioManager.PlayAudioClipOnPlayer(onPickupSound);
        }
    }
}
