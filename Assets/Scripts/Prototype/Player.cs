using System;
using UnityEngine;



public class Player : MonoBehaviour {

    [SerializeField] public Transform cameraRoot;
    [SerializeField] public LayerMask interactMask;
    [SerializeField] public float interactRange;
    [SerializeField] public float startingHealth;
    [SerializeField] private PlayerStatsSO startingStats; // Not Used Except at start
    public PlayerStatsSO PlayerStats { get; private set; }

    public static event Action PlayerDiedEvent;

    // Health
    private float currentHealth;
    public enum HealthChangedTypes { Increase, Decrease, None }
    public static event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    public class OnHealthChangedEventArgs : EventArgs {
        public HealthChangedTypes changeType;
        public float currentHealth;
        public float healthNormalized;
        public OnHealthChangedEventArgs(HealthChangedTypes changeType, float currentHealth, float healthNormalized) {
            this.changeType = changeType;
            this.currentHealth = currentHealth;
            this.healthNormalized = healthNormalized;
        }
    }


    // Start is called before the first frame update
    protected void Awake() {
        PlayerStats = Instantiate(startingStats); // Create a clone
        currentHealth = PlayerStats.HealthMaximum;

    }
    protected void Start() {
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.Increase, currentHealth, GetHealthNormalized()));
    }


    public void Damage(float damage) {

        currentHealth -= damage;

        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.Decrease, currentHealth, GetHealthNormalized()));

        if (currentHealth <= 0) {
            currentHealth = 0;
            PlayerDiedEvent?.Invoke();
            Debug.Log("U died");
            // Die
        }

    }

    public bool TryHeal(float amount) {
        if (currentHealth >= PlayerStats.HealthMaximum) {
            currentHealth = PlayerStats.HealthMaximum;
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.None, currentHealth, GetHealthNormalized()));
            return false; // Cannot Heal, already max
        }

        if (amount + currentHealth >= PlayerStats.HealthMaximum) { //If healing the amount would take us over starting, take us to starting
            currentHealth = PlayerStats.HealthMaximum;
        } else { // Else heal the amount
            currentHealth += amount;
        }
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.Increase, currentHealth, GetHealthNormalized()));

        return true;

    }

    public void HealMax() {
        TryHeal(PlayerStats.HealthMaximum);
    }

    public void RefreshPlayerEvents() {
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.None, currentHealth, GetHealthNormalized()));
    }

    public float GetHealthNormalized() {
        float normalizedHealth = currentHealth / PlayerStats.HealthMaximum;
        return normalizedHealth;
    }
}
