using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Used for loading a new scene in the gameworld, scene must be put in the build settings
 */
public class SceneLoaderDoor : MonoBehaviour, IInteractable {

    [SerializeField] private string scene;
    [SerializeField] private string hoverText;

    public void interact(GameObject player) {
        SceneManager.LoadScene(scene);
    }

    public string onHoverText() {
        return hoverText;
    }
}
