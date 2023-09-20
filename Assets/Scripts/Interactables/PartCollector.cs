using UnityEngine;

public class PartCollector : MonoBehaviour, IInteractable {

    [SerializeField] private ShipPartManager.ShipPart partToUnlock;

    public void interact(GameObject player) {
        GameManager.Instance.ShipPartManager.CollectPart(partToUnlock);
        //GameManager.Instance.ShopManager.AddNewWeapon(newWeapon);
        Debug.Log("Interacting With New Weapon!");
    }

    public string onHoverText() {
        return "Collect Part";
    }
}
