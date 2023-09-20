using UnityEngine;

public class PartDepositor : MonoBehaviour, IInteractable {

    public void interact(GameObject player) {
        GameManager.Instance.ShipPartManager.TryDepositParts();
    }

    public string onHoverText() {
        return "Deposit Parts";
    }
}
