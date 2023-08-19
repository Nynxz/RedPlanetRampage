using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static InventoryUpgradeButton;
using static PlayerInventorySO;

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

[System.Serializable]
public class InventoryUIVars {

    public GameObject InventoryGroup;
    
    public GameObject WeaponOneGroup;
    public GameObject WeaponTwoGroup;
    public GameObject UpgradeOneGroup;
    public GameObject UpgradeTwoGroup;
    public GameObject UpgradeThreeGroup;
    public GameObject InventoryUpgradeListRoot;
    public GameObject InventoryUpgradeUIPrefab;
}
public class UIManager : MonoBehaviour {

    private GameManager gameManager;

    [SerializeField] public Canvas UICanvas;
    [SerializeField] public EventSystem EventSystem;

    // Inspector Fields
    [SerializeField] public InGameUIVars gameUIVars;
    [SerializeField] public ShopUIVars shopUIVars;
    [SerializeField] public InventoryUIVars inventoryUIVars;

    public UpgradeSlotType currentSelectedUpgradeSlot { get; private set; } = UpgradeSlotType.None;

    public static event EventHandler<NewUpgradeSelectedEventArgs> NewUpgradeSelected;
    public class NewUpgradeSelectedEventArgs : EventArgs {
        
    }

    protected void Start() {
        gameManager = GetComponent<GameManager>();

        gameUIVars.interactText.gameObject.SetActive(false); //Disable Interact Text By Default

        shopUIVars.shopCloseButton.onClick.AddListener(CloseShop);

        gameManager.PlayerManager.GetPlayer.OnHealthChanged += Player_OnHealthChanged;
        //SetupInventoryButtons();
        gameManager.InputManager.OptionsPressed += ToggleInventory;

    }

    private void Player_OnHealthChanged(object sender, Player.OnHealthChangedEventArgs e) {
        // Play sounds depending on good or bad
        SetHPBarFill(e.healthNormalized);
        SetHPAmount(e.currentHealth);
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

    public void SetupInventoryButtons() {
        inventoryUIVars.UpgradeOneGroup.GetComponentInChildren<Button>().onClick.AddListener(() => {
            GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().PlayerInventorySO.upgrades.UpgradeOne.DisableAbilities();
            GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().PlayerInventorySO.upgrades.UpgradeOne = null;
            SetupInventoryUI(GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().PlayerInventorySO);
        });
        inventoryUIVars.UpgradeTwoGroup.GetComponentInChildren<Button>().onClick.AddListener(() => { 
            GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().PlayerInventorySO.upgrades.UpgradeTwo.DisableAbilities(); 
            GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().PlayerInventorySO.upgrades.UpgradeTwo = null; 
            SetupInventoryUI(GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().PlayerInventorySO);
        });
        inventoryUIVars.UpgradeThreeGroup.GetComponentInChildren<Button>().onClick.AddListener(() => { 
            GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().PlayerInventorySO.upgrades.UpgradeThree.DisableAbilities(); 
            GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().PlayerInventorySO.upgrades.UpgradeThree = null; 
            SetupInventoryUI(GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().PlayerInventorySO);
        });
    }

    public void SetupInventoryUpgradeList() {
        foreach(Transform listItem in inventoryUIVars.InventoryUpgradeListRoot.transform) {
            Destroy(listItem.gameObject);
        }
        foreach(UpgradeSO upgradeSO in GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().PlayerInventorySO.upgrades.upgradeStorage) {
            UpgradeUIOption uiOption = Instantiate(inventoryUIVars.InventoryUpgradeUIPrefab, inventoryUIVars.InventoryUpgradeListRoot.transform).GetComponent<UpgradeUIOption>();
            uiOption.Setup(upgradeSO);

        }
    }

    public void SelectUpgrade(InventoryUpgradeButton.UpgradeSlotType upgradeSlotType) {
        inventoryUIVars.UpgradeOneGroup.GetComponent<InventoryUpgradeButton>().Deselect();
        inventoryUIVars.UpgradeTwoGroup.GetComponent<InventoryUpgradeButton>().Deselect();
        inventoryUIVars.UpgradeThreeGroup.GetComponent<InventoryUpgradeButton>().Deselect();

        switch (upgradeSlotType) {
            case UpgradeSlotType.One: inventoryUIVars.UpgradeOneGroup.GetComponent<InventoryUpgradeButton>().Select(); break;
            case UpgradeSlotType.Two: inventoryUIVars.UpgradeTwoGroup.GetComponent<InventoryUpgradeButton>().Select(); ; break;
            case UpgradeSlotType.Three: inventoryUIVars.UpgradeThreeGroup.GetComponent<InventoryUpgradeButton>().Select(); ; break;
            default: return;
        }
        currentSelectedUpgradeSlot = upgradeSlotType;
        //NewUpgradeSelected?.Invoke(this, new NewUpgradeSelectedEventArgs { });
    }

    public void RemoveUpgrade(ref UpgradeSO upgradeSO) {
        if (upgradeSO != null) {
            upgradeSO.DisableAbilities();
        }
    }

    public void AddUpgrade(ref UpgradeSO upgradeSO) {
        if (upgradeSO != null) {
            upgradeSO.EnableAbilities();
        }
    }
    public void SetupInventoryUI(PlayerInventorySO currentInventory) {
        if(currentInventory.weapons.weaponOne)
            inventoryUIVars.WeaponOneGroup.GetComponent<Image>().sprite = currentInventory.weapons.weaponOne.weaponSO.weaponShopData.icon;
        if(currentInventory.weapons.weaponTwo)
            inventoryUIVars.WeaponTwoGroup.GetComponent<Image>().sprite = currentInventory.weapons.weaponTwo.weaponSO.weaponShopData.icon;

        inventoryUIVars.UpgradeOneGroup.GetComponent<InventoryUpgradeButton>().SetupButton(currentInventory.upgrades.UpgradeOne);
        inventoryUIVars.UpgradeTwoGroup.GetComponent<InventoryUpgradeButton>().SetupButton(currentInventory.upgrades.UpgradeTwo);
        inventoryUIVars.UpgradeThreeGroup.GetComponent<InventoryUpgradeButton>().SetupButton(currentInventory.upgrades.UpgradeThree);
    }

    public void ToggleInventory() {
        if (!inventoryUIVars.InventoryGroup.activeInHierarchy) {
            GameManager.Instance.InputManager.DisableInput();
            Cursor.visible = true;
            inventoryUIVars.InventoryGroup.SetActive(true);
        } else {
            GameManager.Instance.InputManager.EnableInput();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            inventoryUIVars.InventoryGroup.SetActive(false);
        }

    }

    public void SetHPBarFill(float normalizedHealth) {
        gameUIVars.hpbarImage.fillAmount = normalizedHealth;
    }
    public void SetHPAmount(float healthAmount) {
        Debug.LogWarning($"hp: {((int)healthAmount)}");
        this.gameUIVars.healthText.text = ((int)healthAmount).ToString();
    }
}
