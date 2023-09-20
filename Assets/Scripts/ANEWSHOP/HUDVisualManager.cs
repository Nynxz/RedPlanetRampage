using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Manages all of the components of the Players InGame HUD
/// </summary>
public class HUDVisualManager : MonoBehaviour {
    [SerializeField] private GameObject HUDGroup;

    [SerializeField] private TextMeshProUGUI InteractText;
    [SerializeField] private TextMeshProUGUI MoneyText;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private Image HPBarImage;
    [SerializeField] private TextMeshProUGUI HPText;


    private GameManager gameManager;

    protected void Start() {
        gameManager = GameManager.Instance;
        InteractText.gameObject.SetActive(false);

        PlayerManager.OnHover += PlayerManager_OnHover;
        PlayerManager.UpdateScore += PlayerManager_UpdateScore;
        PlayerManager.UpdateMoney += PlayerManager_UpdateMoney;
        Player.OnHealthChanged += Player_OnHealthChanged;
    }

    // Score and Money
    private void PlayerManager_UpdateMoney(object sender, PlayerManager.UpdateMoneyEventArgs e) {
        MoneyText.text = $"$ {e.moneyAmount}";
    }
    private void PlayerManager_UpdateScore(object sender, PlayerManager.UpdateScoreEventArgs e) {
        ScoreText.text = $"Score: {e.scoreAmount}";
    }

    // Hover Text
    private void PlayerManager_OnHover(object sender, PlayerManager.OnHoverEventArgs e) {
        if (e.hovering && !InteractText.gameObject.activeInHierarchy) {
            InteractText.gameObject.SetActive(true);
            InteractText.text = e.hoverText;
        } else {
            InteractText.gameObject.SetActive(false);
        }
    }

    // HP Bar
    private void Player_OnHealthChanged(object sender, Player.OnHealthChangedEventArgs e) {
        SetHPBarFill(e.healthNormalized);
        SetHPAmount(e.currentHealth);
    }
    private void SetHPBarFill(float normalizedHealth) {
        HPBarImage.fillAmount = normalizedHealth;
    }
    private void SetHPAmount(float healthAmount) {
        Debug.LogWarning($"hp: {(int)healthAmount}");
        HPText.text = ((int)healthAmount).ToString();
    }
}
