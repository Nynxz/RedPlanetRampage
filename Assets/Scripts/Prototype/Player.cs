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

    public float GetHealthNormalized() {
        float normalizedHealth = currentHealth / startingHealth;
        return normalizedHealth;
    }
}
