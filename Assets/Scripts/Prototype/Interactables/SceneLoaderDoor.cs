using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderDoor : Interactable
{

    [SerializeField] private SceneAsset scene;
    [SerializeField] private string hoverText;

    public override void interact(GameObject player) {
        SceneManager.LoadScene(scene.name);
    }

    public override string onHoverText() {
        return hoverText;
    }
}
