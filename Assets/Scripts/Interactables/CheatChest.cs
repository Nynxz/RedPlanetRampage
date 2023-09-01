using UnityEngine;

//An implemntation of the interactable class, used as a debug for testing
public class CheatChest : MonoBehaviour, IInteractable {

    public void interact(GameObject player) {
        player.GetComponent<WeaponManager>().FillAmmo();
        player.GetComponent<Player>().TryHeal(100);
        Debug.Log("Interacting With Cheat Chest!");
    }

    public string onHoverText() {
        return "Cheat Chest";
    }
}
