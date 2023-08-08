using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(InputManager))]
public class GameManager : MonoBehaviour {
    // Singleton Instance
    public static GameManager Instance;


    // Components Needed
    public UIManager UIManager { get; private set; }
    public InputManager InputManager { get; private set; }



    //Testing Branch
    [SerializeField] public Transform playerTransform;



    // Player Variables
    private int currentMoney = 0;
    private int currentScore = 0;


    // Player Variable Update Events
    public class UpdateAmmoArgs {
        public int currentInMag;
        public int maximumInMag;
        public int ammoLeft;
    }

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
            UIManager = GetComponent<UIManager>();
            InputManager = GetComponent<InputManager>();
        }
    }

    void Start()
    {
        // When these events are 'invoked' call the SetText Method
        UpdateMoney += UIManager.SetMoneyText;
        UpdateScore += UIManager.SetScoreText;
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
}
