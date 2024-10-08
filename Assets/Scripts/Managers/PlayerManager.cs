using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Player player;

    [SerializeField] private PlayerUnlocksSO debugStartingUnlocks;
    public PlayerUnlocksSO currentUnlocks { get; private set; }

    public Vector3 PlayerPosition => player.transform.position;
    public Player Player => player;

    private GameObject playerGameObject;

    // Player Variables
    private int currentMoney = 0;
    private int currentScore = 0;
    private int currentKillCount = 0;

    private IInteractable currentInteractable;


    public static event Action<InfoUpdatedEventArgs> InfoUpdatedEvent;

    public class InfoUpdatedEventArgs : EventArgs {
        public int points { get; private set; }
        public int money { get; private set; }
        public int zombieKills { get; private set; }
        public InfoUpdatedEventArgs(int points, int money, int kills) {
            this.points = points;
            this.money = money;
            this.zombieKills = kills;
        }
    }

    #region EVENTS
    public class UpdateAmmoArgs {
        public int currentInMag;
        public int maximumInMag;
        public int ammoLeft;
    }

    public static event EventHandler<UpdateMoneyEventArgs> UpdateMoney;
    public class UpdateMoneyEventArgs : EventArgs {
        public int moneyAmount;
    }

    public static event EventHandler<UpdateScoreEventArgs> UpdateScore;
    public class UpdateScoreEventArgs : EventArgs {
        public int scoreAmount;
    }

    public static event EventHandler<OnHoverEventArgs> OnHover;
    public class OnHoverEventArgs : EventArgs {
        public bool hovering;
        public string hoverText;
    }
    #endregion

    protected void Awake() {
        player = SpawnPlayer();

        currentUnlocks = Instantiate(debugStartingUnlocks); // We would load a json and convert to the correct type
        foreach (WeaponUnlockS weaponUnlockS in currentUnlocks.weapons) {
            if (weaponUnlockS.isUnlocked) {
                Player.GetComponent<WeaponManager>().AddWeaponToInventory(weaponUnlockS.weapon);
            }
        }
        // Spawn the Player

    }

    protected void Start() {
        GameManager.Instance.NewShopManager.LoadShop(currentUnlocks);

        InputManager.OnInteract += TryInteract;

        Zombie.AZombieDiedEvent += Zombie_AZombieDiedEvent;

    }

    private void Zombie_AZombieDiedEvent() {
        currentKillCount++;
        InfoUpdatedEvent?.Invoke(new InfoUpdatedEventArgs(currentScore, currentMoney, currentKillCount));
    }

    protected void Update() {
        // Raycast forward to see if theres an interactable
        if (Physics.Raycast(player.cameraRoot.position, player.cameraRoot.forward, out RaycastHit hit, player.interactRange, player.interactMask)) {
            if (hit.collider.TryGetComponent(out IInteractable interactable)) {
                if (currentInteractable != interactable) {
                    StartHovering(interactable);
                }
            }
        } else if (currentInteractable != null) {
            ClearHover();
        }

    }

    private void TryInteract() {
        if (currentInteractable != null) {
            IInteractable toInteract = currentInteractable;
            currentInteractable = null;
            toInteract.interact(playerGameObject);
        }
    }

    private Player SpawnPlayer() {
        playerGameObject = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        return playerGameObject.GetComponent<Player>();
    }

    public void StartHovering(IInteractable interactable) {
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
        InfoUpdatedEvent?.Invoke(new InfoUpdatedEventArgs(currentScore, currentMoney, currentKillCount));
    }

    public bool TryRemoveMoney(int amount) {
        if (CheckMoneyAmount(amount)) {
            currentMoney -= amount;
            UpdateMoney?.Invoke(this, new UpdateMoneyEventArgs() {
                moneyAmount = currentMoney
            });
            InfoUpdatedEvent?.Invoke(new InfoUpdatedEventArgs(currentScore, currentMoney, currentKillCount));

            return true;
        }
        InfoUpdatedEvent?.Invoke(new InfoUpdatedEventArgs(currentScore, currentMoney, currentKillCount));

        return false;
    }

    private bool CheckMoneyAmount(int amount) {
        return amount <= currentMoney;
    }

    public void AddScore(int amount) {
        currentScore += amount;
        UpdateScore?.Invoke(this, new UpdateScoreEventArgs() {
            scoreAmount = currentScore
        });
        InfoUpdatedEvent?.Invoke(new InfoUpdatedEventArgs(currentScore, currentMoney, currentKillCount));

    }

    public void RemoveScore(int amount) {
        currentScore -= amount;
        UpdateScore?.Invoke(this, new UpdateScoreEventArgs() {
            scoreAmount = currentScore
        });
        InfoUpdatedEvent?.Invoke(new InfoUpdatedEventArgs(currentScore, currentMoney, currentKillCount));
    }

    public InfoUpdatedEventArgs GetGameInfo() {
        return new InfoUpdatedEventArgs(currentScore, currentMoney, currentKillCount);
    }

    protected void OnDrawGizmos() {
        // Interaction Probe
        Gizmos.color = Color.green;
        if (player)
            Gizmos.DrawLine(player.cameraRoot.position, player.cameraRoot.position + player.cameraRoot.forward * player.interactRange);
    }

}
