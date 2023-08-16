using UnityEngine;

// Used for opening the shop interface in the gameworld
public class ShopInteractable : Interactable {
    public override void interact(GameObject player) {
        GameManager.Instance.UIManager.OpenShop();
    }

    public override string onHoverText() {
        return "Open Shop";
    }
}
