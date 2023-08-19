using System;
using UnityEngine;



public class Player : MonoBehaviour {

    [SerializeField] public Transform cameraRoot;
    [SerializeField] public LayerMask interactMask;
    [SerializeField] public float interactRange;
    [SerializeField] public float startingHealth;
    [SerializeField] private PlayerStatsSO startingStats; // Not Used Except at start
    public PlayerStatsSO playerStats { get; private set; }



    // Health
    private float currentHealth;
    public enum HealthChangedTypes { Increase, Decrease, None }
    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
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
    private void Awake() {
        playerStats = Instantiate(startingStats); // Create a clone
        currentHealth = playerStats.HealthMaximum;

    }
    protected void Start() {
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.Increase, currentHealth, GetHealthNormalized()));
    }


    public void Damage(float damage) {

        currentHealth -= damage;

        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.Decrease, currentHealth, GetHealthNormalized()));

        if (currentHealth <= 0) {
            currentHealth = 0;
            Debug.Log("U died");
            // Die
        }

    }

    public bool TryHeal(float amount) {
        if (currentHealth >= playerStats.HealthMaximum) {
            currentHealth = playerStats.HealthMaximum;
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.None, currentHealth, GetHealthNormalized()));
            return false; // Cannot Heal, already max
        }

        if (amount + currentHealth >= playerStats.HealthMaximum) { //If healing the amount would take us over starting, take us to starting
            currentHealth = playerStats.HealthMaximum;
        } else { // Else heal the amount
            currentHealth += amount;
        }
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.Increase, currentHealth, GetHealthNormalized()));

        return true;

    }

    public void RefreshPlayerEvents() {
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.None, currentHealth, GetHealthNormalized()));
    }

    public float GetHealthNormalized() {
        float normalizedHealth = currentHealth / playerStats.HealthMaximum;
        return normalizedHealth;
    }
}
