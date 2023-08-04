using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField] Button playButton;
    [SerializeField] Button highscoreButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button exitButton;

    void Start()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        highscoreButton.onClick.AddListener(HighscoreButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);
    }

    private void PlayButtonClicked() {
        Debug.Log("Start Game");
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
