using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton Instance
    public static GameManager Instance;

    // Inspector Fields
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI scoreText;

    // Player Variables
    private int currentMoney = 0;
    private int currentScore = 0;

    // String Decorators
    private readonly string moneyPrefix = "$ ";
    private readonly string scorePrefix = "Score: ";

    // Player Variable Update Events
    public event EventHandler<UpdateMoneyEventArgs> UpdateMoney;
    public class UpdateMoneyEventArgs : EventArgs {
        public int moneyAmount;
    }

    public event EventHandler<UpdateScoreEventArgs> UpdateScore;
    public class UpdateScoreEventArgs : EventArgs {
        public int scoreAmount;
    }

    private void Awake() {
        // Set Singleton Instance to First Created Instance
        if (Instance != null) {
            Debug.LogWarning("Multiple instances of GameManager.cs found, Please only use one.");
        } else {
            Instance = this;
        }
    }

    void Start()
    {
        // When these events are 'invoked' call the SetText Method
        UpdateMoney += SetMoneyText;
        UpdateScore += SetScoreText;
    }

    // These are called when invoking the Events
    private void SetMoneyText(object sender, UpdateMoneyEventArgs e) {
        moneyText.text = moneyPrefix + e.moneyAmount.ToString();
    }
    private void SetScoreText(object sender, UpdateScoreEventArgs e) {
        scoreText.text = scorePrefix + e.scoreAmount.ToString();
    }

    // These are the functions you want to use to update the score and money


    public void AddMoney(int amount) {
        currentMoney += amount;
        UpdateMoney?.Invoke(this, new UpdateMoneyEventArgs() {
            moneyAmount = currentMoney
        });
    }
    public void RemoveMoney(int amount) {
        if (CheckMoneyAmount(amount)) {
            currentMoney -= amount;
            UpdateMoney?.Invoke(this, new UpdateMoneyEventArgs() {
                moneyAmount = currentMoney
            });
        }
    }
    private bool CheckMoneyAmount(int amount) {
        return amount <= currentMoney;
    }

    public void AddScore(int amount) {
        currentScore += amount;
        UpdateScore?.Invoke(this, new UpdateScoreEventArgs() {
            scoreAmount = currentScore
        });
    }
    public void RemoveScore(int amount) {
        currentScore -= amount;
        UpdateScore?.Invoke(this, new UpdateScoreEventArgs() {
            scoreAmount = currentScore
        });
    }



    private void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            AddMoney(1);
            AddScore(1);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            RemoveMoney(1);
            RemoveScore(1);
        }
    }
}
