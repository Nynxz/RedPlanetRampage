using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Used for unlocking weapons in the gameworld
// TODO: Add Cost
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
