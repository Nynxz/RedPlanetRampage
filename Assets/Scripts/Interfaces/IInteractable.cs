using UnityEngine;

public interface IInteractable {

    public void interact(GameObject player);

    // What to display when the player 'hovers' over the gameobject
    public string onHoverText();

}
