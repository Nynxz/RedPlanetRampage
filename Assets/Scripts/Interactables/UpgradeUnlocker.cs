using UnityEngine;


// Used for unlocking weapons in the gameworld
// TODO: Add Cost?
public class UpgradeUnlocker : Interactable {

    [SerializeField] private UpgradeSO newUpgrade;

    public override void interact(GameObject player) {
        GameManager.Instance.ShopManager.AddNewUpgrade(newUpgrade);
        Debug.Log("Interacting With New Unlocker!");
    }

    public override string onHoverText() {
        return "Unlock: " + newUpgrade.Name;
    }
}
