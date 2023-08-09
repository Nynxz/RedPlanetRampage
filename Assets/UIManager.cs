using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class UIManager : MonoBehaviour {

    private GameManager gameManager;

    [SerializeField] public Canvas UICanvas;

    // Inspector Fields
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI interactText;

    [Header ("HP Bar")]
    [SerializeField] private Image hpbarImage;
    [SerializeField] private TextMeshProUGUI healthAmount;

    [Header("Visual Prefabs")]
    [SerializeField] public GameObject hitmarkerRegular;
    [SerializeField] public GameObject hitmarkerHeadshot;

    // String Decorators
    private readonly string moneyPrefix = "$ ";
    private readonly string scorePrefix = "Score: ";

    void Start() {
        gameManager = GetComponent<GameManager>();

        interactText.gameObject.SetActive(false); //Disable Interact Text By Default
    }

    public void SetMoneyText(object sender, PlayerManager.UpdateMoneyEventArgs e) {
        moneyText.text = moneyPrefix + e.moneyAmount.ToString();
    }
    public void SetScoreText(object sender, PlayerManager.UpdateScoreEventArgs e) {
        scoreText.text = scorePrefix + e.scoreAmount.ToString();
    }
    public void SetAmmoText(PlayerManager.UpdateAmmoArgs e) {
        SetAmmoText(e.currentInMag.ToString() + "/" + e.maximumInMag.ToString() + " | " + e.ammoLeft.ToString());
    }
    public void SetAmmoText(string text) {
        ammoText.text = text;
    }
    public void SetHoverText(object sender, PlayerManager.OnHoverEventArgs e) {
        if (e.hovering && !interactText.gameObject.activeInHierarchy) {
            interactText.gameObject.SetActive(true);
            interactText.text = e.hoverText;
        } else {
            interactText.gameObject.SetActive(false);
        }
    }
    public void SetHPBarFill(float normalizedHealth) {
        hpbarImage.fillAmount = normalizedHealth;
    }
    public void SetHPAmount(int healthAmount) {
        this.healthAmount.text = healthAmount.ToString();
    }
}
