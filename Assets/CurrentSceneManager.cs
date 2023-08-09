using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    // list
    // serializable scene reference for "coming from"
    // transform for where to place the player
    [System.Serializable]
    public struct SceneEntrances {
        public SceneAsset fromScene;
        public Transform entrancePoint;
    }

    [SerializeField] List<SceneEntrances> sceneList = new List<SceneEntrances>();

    // Start is called before the first frame update
    void Start() {
        PositionPlayer();
    }

    private void PositionPlayer() {
        sceneList.ForEach((scene) => {
            if(scene.fromScene.name == GameManager.previousScene) {
                Debug.Log("Trying to position to point from: " + scene.fromScene + scene.entrancePoint.position);
                Debug.Log(GameManager.previousScene + " --> " + GameManager.currentScene);

                GameManager.Instance.PlayerManager.GetPlayer.GetComponent<CharacterController>().enabled = false;
                GameManager.Instance.PlayerManager.GetPlayer.transform.position = scene.entrancePoint.position;
                GameManager.Instance.PlayerManager.GetPlayer.transform.rotation = scene.entrancePoint.rotation;
                GameManager.Instance.PlayerManager.GetPlayer.GetComponent<CharacterController>().enabled = true;
                return;
            }
        });
        Debug.Log("Could Not Find an Entrance, player will be placed at last scenes position");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
