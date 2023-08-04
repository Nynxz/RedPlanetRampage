using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private Button playButton;
    [SerializeField] private Button highscoreButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    void Start()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        highscoreButton.onClick.AddListener(HighscoreButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);
    }

    private void PlayButtonClicked() {
        Debug.Log("Start Game");
        SceneManager.LoadScene("LabHub");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void HighscoreButtonClicked() {
        Debug.Log("High Scores");
    }

    private void SettingsButtonClicked() {
        Debug.Log("Settings Menu");
    }

    private void ExitButtonClicked() {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
