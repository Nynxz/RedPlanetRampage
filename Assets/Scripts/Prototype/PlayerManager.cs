using StarterAssets;
using System;
using UnityEngine;
using static GameManager;
using static UnityEngine.InputSystem.InputSettings;

public class PlayerManager : MonoBehaviour {

    private GameManager gameManager;
    [SerializeField] private Player player;

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



    void Start() {
        gameManager = GameManager.Instance;
        UpdateMoney += gameManager.UIManager.SetMoneyText;
        UpdateScore += gameManager.UIManager.SetScoreText;
    }

    void Update() {
        if (gameManager.InputManager.interactInput) {
            RaycastHit hit;
            gameManager.InputManager.SetInteract(false);
            if (Physics.Raycast(player.cameraRoot.position, player.cameraRoot.forward, out hit, player.interactRange, player.interactMask)) {
                Debug.Log("Got Hit");

                if (hit.collider.TryGetComponent(out Interactable interactable)) {
                    interactable.interact(gameObject);
                }
            }
        }
    }

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

    private void OnDrawGizmos() {

        // Interaction Probe
        Gizmos.color = Color.green;
        Gizmos.DrawLine(player.cameraRoot.position, player.cameraRoot.position + player.cameraRoot.forward * player.interactRange);
    }
}
