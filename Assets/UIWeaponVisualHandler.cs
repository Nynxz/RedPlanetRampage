using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponVisualHandler : MonoBehaviour {

    [SerializeField] private Image weaponOneImage;
    [SerializeField] private Image weaponTwoImage;

    void Start() {
        WeaponManager.OnInventoryChanged += WeaponManager_OnInventoryChanged;
        UpdateImages(GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>().PlayerInventorySO);
    }

    private void WeaponManager_OnInventoryChanged(PlayerInventorySO obj) {
        UpdateImages(obj);
    }

    private void UpdateImages(PlayerInventorySO invSO) {
        UpdateImages(invSO.weapons.weaponOne?.weaponSO, invSO.weapons.weaponTwo?.weaponSO);
    }

    public void UpdateImages(WeaponSO w1,  WeaponSO w2) {
        if(w1)
            weaponOneImage.sprite = w1.weaponShopData.icon;
        if(w2)
            weaponTwoImage.sprite = w2.weaponShopData.icon;
    }
}
