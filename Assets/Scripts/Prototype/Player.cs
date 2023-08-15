using UnityEngine;



public class Player : MonoBehaviour {

    [SerializeField] public Transform cameraRoot;
    [SerializeField] public LayerMask interactMask;
    [SerializeField] public float interactRange;


    [SerializeField] public float startingHealth;
    private float currentHealth;


    // Start is called before the first frame update
    void Start() {
        currentHealth = startingHealth;
        GameManager.Instance.UIManager.SetHPBarFill(GetHealthNormalized());
        GameManager.Instance.UIManager.SetHPAmount((int)currentHealth);
    }

    // Update is called once per frame
    void Update() {

    }


    public void Damage(float damage) {

        currentHealth -= damage;

        if (currentHealth <= 0) {
            currentHealth = 0;
            Debug.Log("U died");
            // Die
        }

        GameManager.Instance.UIManager.SetHPBarFill(GetHealthNormalized());
        GameManager.Instance.UIManager.SetHPAmount((int)currentHealth);
    }

    public bool TryHeal(float amount) {
        if (currentHealth >= startingHealth) return false; // Cannot Heal, already max

        if(amount + currentHealth >= startingHealth) { //If healing the amount would take us over starting, take us to starting
            currentHealth = startingHealth;
        } else { // Else heal the amount
            currentHealth += amount;
        }

        GameManager.Instance.UIManager.SetHPBarFill(GetHealthNormalized());
        GameManager.Instance.UIManager.SetHPAmount((int)currentHealth); 
        
        return true;

    }

    public float GetHealthNormalized() {
        float normalizedHealth = currentHealth / startingHealth;
        return normalizedHealth;
    }
}
