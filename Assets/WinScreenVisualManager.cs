using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenVisualManager : MonoBehaviour
{
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button CreditsButton;

    private void Start() {
        gameObject.SetActive(false);
        MainMenuButton.onClick.AddListener(() => {
            GameManager.EnableGame();
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            GameManager.EndGame();
        });

        CreditsButton.onClick.AddListener(() => {
            GameManager.EnableGame();
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            GameManager.EndGame();
        });

        ShipPartManager.GameCompletedEvent += OpenWinScreen;
    }

    public void OpenWinScreen() {
        GameManager.DisableGame();
        gameObject.SetActive(true);
    }
}
