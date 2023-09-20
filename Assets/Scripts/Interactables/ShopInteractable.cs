using UnityEngine;

// Used for opening the shop interface in the gameworld
public class ShopInteractable : MonoBehaviour, IInteractable {
    public void interact(GameObject player) {
        NewUIManager.EmitShopTryOpen();
        //GameManager.Instance.UIManager.OpenShop();
    }

    public string onHoverText() {
        return "Open Shop";
    }
}
