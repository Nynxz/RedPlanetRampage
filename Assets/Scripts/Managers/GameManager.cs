using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(InputManager))]
public class GameManager : MonoBehaviour {


    // Singleton Instance
    public static GameManager Instance;


    // Components Needed
    public UIManager UIManager { get; private set; }
    public InputManager InputManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public ShopManager ShopManager { get; private set; }

    public static string previousScene;
    public static string currentScene;


    private void Awake() {
        GameObject.DontDestroyOnLoad(this);

        // Set Singleton Instance to First Created Instance
        if (Instance != null) {
            Debug.LogWarning("Multiple instances of GameManager.cs found, Please only use one.");
        } else {
            Debug.Log("Initing GameManager");
            Instance = this;
            UIManager = GetComponent<UIManager>();
            InputManager = GetComponent<InputManager>();
            PlayerManager = GetComponent<PlayerManager>();
            ShopManager = GetComponent<ShopManager>();
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }
    }

    private void SceneManager_activeSceneChanged(Scene current, Scene next) {
        if (current.name == null) {
            Debug.Log("Current Scene deleted");
            previousScene = currentScene;
            currentScene = next.name;
        } else {
            previousScene = currentScene;
            currentScene = next.name;

        }
        Debug.Log(previousScene + " --> " + currentScene);
    }

    private void Start() {
        GameObject.DontDestroyOnLoad(PlayerManager.GetPlayer.gameObject);
        GameObject.DontDestroyOnLoad(UIManager.UICanvas.gameObject);
        GameObject.DontDestroyOnLoad(UIManager.EventSystem.gameObject);
    }

}
