using UnityEngine;


// Used for unlocking weapons in the gameworld
// TODO: Add Cost?
public class UpgradeUnlocker : MonoBehaviour, IInteractable {

    [SerializeField] private UpgradeSO newUpgrade;

    public void interact(GameObject player) {
        GameManager.Instance.ShopManager.AddNewUpgrade(newUpgrade);
        Debug.Log("Interacting With New Unlocker    !");
    }

    public string onHoverText() {
        return "Unlock: " + newUpgrade.Name;
    }
}
