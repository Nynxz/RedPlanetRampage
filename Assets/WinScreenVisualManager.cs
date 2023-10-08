using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenVisualManager : MonoBehaviour
{
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private TextMeshProUGUI winText;

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
        PlayerManager.InfoUpdatedEventArgs gameInfo = GameManager.Instance.PlayerManager.GetGameInfo();
        winText.text = $"You scored {gameInfo.points} points\r\nYou killed {gameInfo.zombieKills} zombies\r\n";
    }
}
