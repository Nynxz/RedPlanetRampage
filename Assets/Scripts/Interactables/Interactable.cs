using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * An abstract class for gameobjects in the world that are interactable
 */
public abstract class Interactable : MonoBehaviour
{

    // What to do when the player 'interacts' with the gameobject
    public abstract void interact(GameObject player);

    // What to display when the player 'hovers' over the gameobject
    public abstract string onHoverText();
}
