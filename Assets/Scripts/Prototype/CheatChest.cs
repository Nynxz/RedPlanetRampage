using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatChest : Interactable {
    public override void interact(GameObject player) {
        player.GetComponent<WeaponManager>().FillAmmo();
        Debug.Log("Interacting With Cheat Chest!");
    }
}
