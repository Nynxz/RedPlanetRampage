using System;
using System.Collections;
using UnityEngine;



public class Player : MonoBehaviour
{
    public static event Action<bool> IsSlowedEvent;

    public AudioSource footstepSound, sprintSound, jumpSound;

    public AudioSource WeaponSoundSource;

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
    public class OnHealthChangedEventArgs : EventArgs
    {
        public HealthChangedTypes changeType;
        public float currentHealth;
        public float healthNormalized;
        public OnHealthChangedEventArgs(HealthChangedTypes changeType, float currentHealth, float healthNormalized)
        {
            this.changeType = changeType;
            this.currentHealth = currentHealth;
            this.healthNormalized = healthNormalized;
        }
    }

    private bool isSlowed = false;
    private float slowTime = 0f;
    private float originalMoveSpeedMod = 0f;
    private float originalSprintSpeedMod = 0f;

    // Start is called before the first frame update
    protected void Awake()
    {
        PlayerStats = Instantiate(startingStats); // Create a clone
        currentHealth = PlayerStats.HealthMaximum;

    }
    protected void Start()
    {
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.Increase, currentHealth, GetHealthNormalized()));
    }
    private void Update()
    {
        if (slowTime > 0)
        {
            if (!isSlowed)
            {
                IsSlowedEvent?.Invoke(true);
                isSlowed = true;
                originalMoveSpeedMod = PlayerStats.MoveSpeedModifier;
                originalSprintSpeedMod = PlayerStats.SprintSpeedModifier;
                PlayerStats.MoveSpeedModifier *= 0.5f;
                PlayerStats.SprintSpeedModifier *= 0.5f;
            }
            slowTime -= Time.deltaTime;
        }
        else
        {
            if (isSlowed)
            {
                slowTime = 0;
                isSlowed = false;
                IsSlowedEvent?.Invoke(false);
                PlayerStats.MoveSpeedModifier = originalMoveSpeedMod;
                PlayerStats.SprintSpeedModifier = originalSprintSpeedMod;
            }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                footstepSound.enabled = false;
                sprintSound.enabled = true;
            }
            else
            {
                footstepSound.enabled = true;
                sprintSound.enabled = false;
            }
        }
        else
        {
            footstepSound.enabled = false;
            sprintSound.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpSound.enabled = true;
            jumpSound.Play();
        }

    }

    public void Damage(float damage)
    {

        currentHealth -= damage;

        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.Decrease, currentHealth, GetHealthNormalized()));

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            PlayerDiedEvent?.Invoke();
            Debug.Log("U died");
            // Die
        }

    }

    public void AddSlowDebuff(float seconds)
    {
        slowTime += seconds;
    }


    public bool TryHeal(float amount)
    {
        if (currentHealth >= PlayerStats.HealthMaximum)
        {
            currentHealth = PlayerStats.HealthMaximum;
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.None, currentHealth, GetHealthNormalized()));
            return false; // Cannot Heal, already max
        }

        if (amount + currentHealth >= PlayerStats.HealthMaximum)
        { //If healing the amount would take us over starting, take us to starting
            currentHealth = PlayerStats.HealthMaximum;
        }
        else
        { // Else heal the amount
            currentHealth += amount;
        }
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.Increase, currentHealth, GetHealthNormalized()));

        return true;

    }

    public void HealMax()
    {
        TryHeal(PlayerStats.HealthMaximum);
    }

    public void RefreshPlayerEvents()
    {
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(HealthChangedTypes.None, currentHealth, GetHealthNormalized()));
    }

    public float GetHealthNormalized()
    {
        float normalizedHealth = currentHealth / PlayerStats.HealthMaximum;
        return normalizedHealth;
    }
}
