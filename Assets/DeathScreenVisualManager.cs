using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenVisualManager : MonoBehaviour
{
    [SerializeField] private Button MainMenu;
    [SerializeField] private Button RestartButton;
    [SerializeField] private TextMeshProUGUI deathText;

    private void Start() {
        gameObject.SetActive(false);
        MainMenu.onClick.AddListener(() => {

        });
        RestartButton.onClick.AddListener(() => {
            GameManager.Instance.MissionManager.LeaveMission();
            gameObject.SetActive(false);
            GameManager.EnableGame();
            GameManager.Instance.PlayerManager.Player.HealMax();
        });
    }

    public void OpenDeathScreen() {
        gameObject.SetActive(true);
        PlayerManager.InfoUpdatedEventArgs gameInfo = GameManager.Instance.PlayerManager.GetGameInfo();
        deathText.text = $"You scored {gameInfo.points} points\r\nYou killed {gameInfo.zombieKills} zombies\r\n";
    }
}
