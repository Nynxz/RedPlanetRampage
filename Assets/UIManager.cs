using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

[System.Serializable]
public class InGameUIVars {

    public GameObject InGameGroup;

    [Header("Text")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI interactText;

    [Header("HP Bar")]
    public Image hpbarImage;
    public TextMeshProUGUI healthText;

    [Header("Visul Prefabs")]
    public GameObject hitmarkerRegular;
    public GameObject hitmarkerHeadshot;

    // String Decorators
    public readonly string moneyPrefix = "$ ";
    public readonly string scorePrefix = "Score: ";
}

[System.Serializable]
public class ShopUIVars {

    public GameObject ShopGroup;
    public Button shopCloseButton;
    public GameObject VerticalShopButtonGroup;
    public GameObject ShopOptionPrefab;
}

public class UIManager : MonoBehaviour {

    private GameManager gameManager;

    [SerializeField] public Canvas UICanvas;


    // Inspector Fields

    [SerializeField] public InGameUIVars gameUIVars;
    [SerializeField] public ShopUIVars shopUIVars;

/*    [Header("Ingame -----")]
    [SerializeField] private GameObject inGameGroup;
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

    [Header("Shop -----")]
    [SerializeField] private GameObject shopGroup;
*/


    void Start() {
        gameManager = GetComponent<GameManager>();

        gameUIVars.interactText.gameObject.SetActive(false); //Disable Interact Text By Default

        shopUIVars.shopCloseButton.onClick.AddListener(CloseShop);
    }

    private void CloseShop() {
        shopUIVars.ShopGroup.gameObject.SetActive(false);
        gameUIVars.InGameGroup.gameObject.SetActive(true);

        GameManager.Instance.InputManager.EnableInput();
        Cursor.visible = false;

        GameManager.Instance.InputManager.OptionsPressed -= CloseShop;
    }
    public void OpenShop() {
        shopUIVars.ShopGroup.gameObject.SetActive(true);
        gameUIVars.InGameGroup.gameObject.SetActive(false);

        GameManager.Instance.InputManager.DisableInput();
        Cursor.visible = true;

        GameManager.Instance.InputManager.OptionsPressed += CloseShop;
    }

    public void SetMoneyText(object sender, PlayerManager.UpdateMoneyEventArgs e) {
        gameUIVars.moneyText.text = gameUIVars.moneyPrefix + e.moneyAmount.ToString();
    }
    public void SetScoreText(object sender, PlayerManager.UpdateScoreEventArgs e) {
        gameUIVars.scoreText.text = gameUIVars.scorePrefix + e.scoreAmount.ToString();
    }
    public void SetAmmoText(PlayerManager.UpdateAmmoArgs e) {
        SetAmmoText(e.currentInMag.ToString() + "/" + e.maximumInMag.ToString() + " | " + e.ammoLeft.ToString());
    }
    public void SetAmmoText(string text) {
        gameUIVars.ammoText.text = text;
    }
    public void SetHoverText(object sender, PlayerManager.OnHoverEventArgs e) {
        if (e.hovering && !gameUIVars.interactText.gameObject.activeInHierarchy) {
            gameUIVars.interactText.gameObject.SetActive(true);
            gameUIVars.interactText.text = e.hoverText;
        } else {
            gameUIVars.interactText.gameObject.SetActive(false);
        }
    }
    public void SetHPBarFill(float normalizedHealth) {
        gameUIVars.hpbarImage.fillAmount = normalizedHealth;
    }
    public void SetHPAmount(int healthAmount) {
        this.gameUIVars.healthText.text = healthAmount.ToString();
    }
}
