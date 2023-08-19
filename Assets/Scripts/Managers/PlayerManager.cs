using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Player player;

    public Vector3 PlayerPosition => player.transform.position;
    public Player Player => player;

    private GameObject playerGameObject;
    private GameManager gameManager;

    // Player Variables
    private int currentMoney = 0;
    private int currentScore = 0;

    private Interactable currentInteractable;


    #region EVENTS
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

    public event EventHandler<OnHoverEventArgs> OnHover;
    public class OnHoverEventArgs : EventArgs {
        public bool hovering;
        public string hoverText;
    }
    #endregion

    protected void Awake() {
        // Spawn the Player
        player = SpawnPlayer();
    }

    protected void Start() {
        gameManager = GameManager.Instance;
        UpdateMoney += gameManager.UIManager.SetMoneyText;
        UpdateScore += gameManager.UIManager.SetScoreText;
        OnHover += gameManager.UIManager.SetHoverText;

        GameManager.Instance.InputManager.OnInteract += TryInteract;
    }

    protected void Update() {
        // Raycast forward to see if theres an interactable
        if (Physics.Raycast(player.cameraRoot.position, player.cameraRoot.forward, out RaycastHit hit, player.interactRange, player.interactMask)) {
            if (hit.collider.TryGetComponent(out Interactable interactable)) {
                if (currentInteractable != interactable) {
                    StartHovering(interactable);
                }
            }
        } else if(currentInteractable) {
            ClearHover();
        }
 
    }

    private void TryInteract() {
        if (currentInteractable) {
            Interactable toInteract = currentInteractable;
            currentInteractable = null;
            toInteract.interact(playerGameObject);
        }
    }

    private Player SpawnPlayer() {
        playerGameObject = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        return playerGameObject.GetComponent<Player>();
    }

    public void StartHovering(Interactable interactable) {
        OnHover?.Invoke(this, new OnHoverEventArgs() {
            hovering = true,
            hoverText = interactable.onHoverText()
        });
        currentInteractable = interactable;
    }

    public void ClearHover() {
        Debug.Log("Clearing Hovering");
        OnHover?.Invoke(this, new OnHoverEventArgs() {
            hovering = false,
            hoverText = "You shouldn't be seeing this"
        });
        currentInteractable = null;
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

    protected void OnDrawGizmos() {

        // Interaction Probe
        Gizmos.color = Color.green;
        if (player)
            Gizmos.DrawLine(player.cameraRoot.position, player.cameraRoot.position + player.cameraRoot.forward * player.interactRange);
    }
}
