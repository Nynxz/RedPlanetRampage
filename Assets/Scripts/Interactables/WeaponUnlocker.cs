using UnityEngine;


// Used for unlocking weapons in the gameworld
// TODO: Add Cost?
public class WeaponUnlocker : MonoBehaviour, IInteractable {

    [SerializeField] private WeaponSO newWeapon;

    public void interact(GameObject player) {
        GameManager.Instance.ShopManager.AddNewWeapon(newWeapon);
        Debug.Log("Interacting With New Weapon!");
    }

    public string onHoverText() {
        return "Unlock: " + newWeapon.weaponData.weaponName;
    }
}
