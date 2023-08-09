using System;
using UnityEngine;


[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(InputManager))]
public class GameManager : MonoBehaviour {


    // Singleton Instance
    public static GameManager Instance;


    // Components Needed
    public UIManager UIManager { get; private set; }
    public InputManager InputManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }


    private void Awake() {
        // Set Singleton Instance to First Created Instance
        if (Instance != null) {
            Debug.LogWarning("Multiple instances of GameManager.cs found, Please only use one.");
        } else {
            Instance = this;
            UIManager = GetComponent<UIManager>();
            InputManager = GetComponent<InputManager>();
            PlayerManager = GetComponent<PlayerManager>();
        }
    }
}
