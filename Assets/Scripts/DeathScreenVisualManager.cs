using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenVisualManager : MonoBehaviour
{
    [SerializeField] private Button MainMenu;
    [SerializeField] private Button RestartButton;

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
    }
}
