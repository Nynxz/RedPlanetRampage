using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {
    protected abstract bool TryOnPickup(Player player);
    protected ItemSO itemSO { get; private set; }

    public void Init(ItemSO itemSO) {
        this.itemSO = itemSO;
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.TryGetComponent(out Player player)) {
            if (TryOnPickup(player)) {
                Destroy(gameObject);
            }
        }
    }

}
