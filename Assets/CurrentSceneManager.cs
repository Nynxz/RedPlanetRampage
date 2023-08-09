using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentSceneManager : MonoBehaviour
{


    [SerializeField] private bool loadManagersDebug = false;

    [System.Serializable]
    public struct SceneEntrances {
        public SceneAsset fromScene;
        public Transform entrancePoint;
    }

    [SerializeField] List<SceneEntrances> sceneList = new List<SceneEntrances>();


    private void Awake() {
        if (loadManagersDebug && !GameManager.Instance) {
            SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            GameManager.currentScene = SceneManager.GetActiveScene().name;
            Debug.Log(GameManager.currentScene);

        }
    }

    // Start is called before the first frame update
    void Start() {
        Debug.Log("Starting");
        PositionPlayer();
    }

    private void PositionPlayer() {
        foreach(SceneEntrances scene in sceneList) {
            if (scene.fromScene.name == GameManager.previousScene) {
                Debug.Log("Trying to position to point from: " + scene.fromScene + scene.entrancePoint.position);
                Debug.Log(GameManager.previousScene + " --> " + GameManager.currentScene);

                GameManager.Instance.PlayerManager.GetPlayer.GetComponent<CharacterController>().enabled = false;
                GameManager.Instance.PlayerManager.GetPlayer.transform.position = scene.entrancePoint.position;
                GameManager.Instance.PlayerManager.GetPlayer.transform.rotation = scene.entrancePoint.rotation;
                GameManager.Instance.PlayerManager.GetPlayer.GetComponent<CharacterController>().enabled = true;
                return;
            }
        }
        Debug.Log("Could Not Find an Entrance, player will be placed at last scenes position");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
