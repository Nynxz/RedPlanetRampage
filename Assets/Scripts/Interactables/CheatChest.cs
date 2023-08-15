using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    An implemntation of the interactable class,
    used as a debug for testing
 */
public class CheatChest : Interactable {

    public override void interact(GameObject player) {
        player.GetComponent<WeaponManager>().FillAmmo();
        player.GetComponent<Player>().TryHeal(100);
        Debug.Log("Interacting With Cheat Chest!");
    }

    public override string onHoverText() {
        return "Cheat Chest";
    }
}
