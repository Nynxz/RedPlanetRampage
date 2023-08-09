using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractable : Interactable
{
    public override void interact(GameObject player) {
        GameManager.Instance.UIManager.OpenShop();
    }

    public override string onHoverText() {
        return "Open Shop";
    }
}
