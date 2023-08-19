using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script supposed to be placed in each scene, used for setting entrances of the scene and for startup settings
public class CurrentSceneManager : MonoBehaviour {

    // Used for force loading the manager scene without needing to start the game through main menu
    //[SerializeField] private bool loadManagersDebug = false;

    // Struct used for creating a list of entrances, 'when I come from scene X, go to position Y'
    [System.Serializable]
    public struct SceneEntrances {
        public SceneAsset fromScene;
        public Transform entrancePoint;
    }

    // The list of entrances from other scenes
    [SerializeField] List<SceneEntrances> sceneList = new List<SceneEntrances>();


    protected void Awake() {
        if (!GameManager.Instance) {
            SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            GameManager.currentScene = SceneManager.GetActiveScene().name;
            Debug.LogWarning("Loaded Manager Scene");

        }
    }

    protected void Start() {
        PositionPlayer();
    }

    private void PositionPlayer() {
        foreach (SceneEntrances sceneEntrance in sceneList) {
            if (sceneEntrance.fromScene.name == GameManager.previousScene) {
                Debug.Log("Trying to position to point from: " + sceneEntrance.fromScene + sceneEntrance.entrancePoint.position);
                Debug.Log(GameManager.previousScene + " --> " + GameManager.currentScene);

                GameManager.Instance.PlayerManager.Player.GetComponent<CharacterController>().enabled = false;
                GameManager.Instance.PlayerManager.Player.transform.position = sceneEntrance.entrancePoint.position;
                GameManager.Instance.PlayerManager.Player.transform.rotation = sceneEntrance.entrancePoint.rotation;
                GameManager.Instance.PlayerManager.Player.GetComponent<CharacterController>().enabled = true;

                return;
            }
        }
        Debug.LogWarning("Could Not Find an Entrance, player will be placed at last scenes position");
    }

}
