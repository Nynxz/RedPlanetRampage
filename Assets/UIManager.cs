using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;

public class UIManager : MonoBehaviour {

    private GameManager gameManager;

    [SerializeField] public Canvas UICanvas;

    // Inspector Fields
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Visual Prefabs")]
    [SerializeField] public GameObject hitmarkerRegular;
    [SerializeField] public GameObject hitmarkerHeadshot;

    // String Decorators
    private readonly string moneyPrefix = "$ ";
    private readonly string scorePrefix = "Score: ";

    void Start() {
        gameManager = GetComponent<GameManager>();    
    }

    public void SetMoneyText(object sender, UpdateMoneyEventArgs e) {
        moneyText.text = moneyPrefix + e.moneyAmount.ToString();
    }
    public void SetScoreText(object sender, UpdateScoreEventArgs e) {
        scoreText.text = scorePrefix + e.scoreAmount.ToString();
    }
    public void SetAmmoText(UpdateAmmoArgs e) {
        SetAmmoText(e.currentInMag.ToString() + "/" + e.maximumInMag.ToString() + " | " + e.ammoLeft.ToString());
    }
    public void SetAmmoText(string text) {
        ammoText.text = text;
    }
}
