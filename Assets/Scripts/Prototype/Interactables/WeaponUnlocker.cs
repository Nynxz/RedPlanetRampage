using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlocker : Interactable {

    [SerializeField] private WeaponSO newWeapon;

    public override void interact(GameObject player) {
        GameManager.Instance.ShopManager.AddNewWeapon(newWeapon);
        Debug.Log("Interacting With New Weapon!");
    }

    public override string onHoverText() {
        return "Unlock: " + newWeapon.weaponData.weaponName;
    }
}
