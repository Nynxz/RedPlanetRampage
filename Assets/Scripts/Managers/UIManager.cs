using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static InventoryUpgradeButton;
using static InventoryWeaponButton;


/// <summary>
/// THIS IS IN THE PROCESS OF BEING REPLACED BY SEPERATE SCRIPTS
/// </summary>
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
    public GameObject NewShopGroup;



    public GameObject ShopGroup;
    public Button shopCloseButton;
    public GameObject VerticalShopButtonGroupWeapons;
    public GameObject VerticalShopButtonGroupUpgrades;
    public GameObject ShopOptionPrefabWeapon;
    public GameObject ShopOptionPrefabUpgrade;
    public Button ShopTypeSwitchButton;
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
    public GameObject InventoryWeaponListRoot;
    public GameObject InventoryWeaponUIPrefab;
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
    public WeaponSlotType  currentSelectedWeaponSlot { get; private set; } = WeaponSlotType.None;

    [SerializeField] private TextMeshProUGUI invHeader;



    protected void Start() {
        gameManager = GetComponent<GameManager>();

        /*Coverted*/
        gameUIVars.interactText.gameObject.SetActive(false); //Disable Interact Text By Default


        shopUIVars.ShopTypeSwitchButton.onClick.AddListener(() => {
            ShopManager.CurrentShop currentShop = GameManager.Instance.ShopManager.SwitchBuyMenus();
            shopUIVars.ShopTypeSwitchButton.GetComponentInChildren<TextMeshProUGUI>().text = currentShop.ToString();
        });
        ShopManager.CurrentShop currentShop = GameManager.Instance.ShopManager.SwitchBuyMenus();
        shopUIVars.ShopTypeSwitchButton.GetComponentInChildren<TextMeshProUGUI>().text = currentShop.ToString();



        //SetupInventoryButtons();


        /*Coverted*/
        Player.OnHealthChanged += Player_OnHealthChanged;

        shopUIVars.shopCloseButton.onClick.AddListener(CloseShop);

        InputManager.OptionsPressed += ToggleInventory;

        WeaponManager.OnInventoryChanged += SetupInventoryUI;
        WeaponManager.OnInventoryChanged += SetupInventoryUpgradeList;

        /*Coverted*/
        PlayerManager.UpdateMoney += SetMoneyText;
        PlayerManager.UpdateScore += SetScoreText;

        /*Coverted*/
        PlayerManager.OnHover += SetHoverText;


        // Duct tape fixing before final
    }

    private void Player_OnHealthChanged(object sender, Player.OnHealthChangedEventArgs e) {
        // Play sounds depending on good or bad
        SetHPBarFill(e.healthNormalized);
        SetHPAmount(e.currentHealth);
    }

    private void CloseShop() {
        shopUIVars.NewShopGroup.gameObject.SetActive(false);
        //shopUIVars.ShopGroup.gameObject.SetActive(false);
        gameUIVars.InGameGroup.gameObject.SetActive(true);

        GameManager.Instance.InputManager.EnableInput();
        Cursor.visible = false;

        InputManager.OptionsPressed -= CloseShop;
        InputManager.OptionsPressed += ToggleInventory;

    }
    public void OpenShop() {
        InputManager.OptionsPressed -= ToggleInventory;
        shopUIVars.NewShopGroup.gameObject.SetActive(true);
        //shopUIVars.ShopGroup.gameObject.SetActive(true);
        gameUIVars.InGameGroup.gameObject.SetActive(false);
        GameManager.Instance.NewShopManager.RefreshShop();
        GameManager.Instance.InputManager.DisableInput();
        Cursor.visible = true;

        InputManager.OptionsPressed += CloseShop;
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

    public void SetupInventoryUpgradeList(PlayerInventorySO currentInventory) {
        foreach (Transform listItem in inventoryUIVars.InventoryUpgradeListRoot.transform) {
            Destroy(listItem.gameObject);
        }
        for (int i = 0; i < currentInventory.upgrades.upgradeStorage.Count; i++) {
            UpgradeUIOption uiOption = Instantiate(inventoryUIVars.InventoryUpgradeUIPrefab, inventoryUIVars.InventoryUpgradeListRoot.transform).GetComponent<UpgradeUIOption>();
            uiOption.Setup(currentInventory.upgrades.upgradeStorage[i], i);
        }

        foreach (Transform listItem in inventoryUIVars.InventoryWeaponListRoot.transform) {
            Destroy(listItem.gameObject);
        }
        for (int i = 0; i < currentInventory.weapons.equippedStorage.Count; i++) {
            WeaponUIOption uiOption = Instantiate(inventoryUIVars.InventoryWeaponUIPrefab, inventoryUIVars.InventoryWeaponListRoot.transform).GetComponent<WeaponUIOption>();
            uiOption.Setup(currentInventory.weapons.equippedStorage[i], i);
        }
    }



    public void SelectUpgradeSlot(UpgradeSlotType upgradeSlotType) {
        DeselectUpgradeSlots();
        DeselectWeaponSlots();

        inventoryUIVars.InventoryUpgradeListRoot.SetActive(true);
        inventoryUIVars.InventoryWeaponListRoot.SetActive(false);
        invHeader.text = "Upgrades";

        switch (upgradeSlotType) {
            case UpgradeSlotType.One: inventoryUIVars.UpgradeOneGroup.GetComponent<InventoryUpgradeButton>().Select(); break;
            case UpgradeSlotType.Two: inventoryUIVars.UpgradeTwoGroup.GetComponent<InventoryUpgradeButton>().Select(); ; break;
            case UpgradeSlotType.Three: inventoryUIVars.UpgradeThreeGroup.GetComponent<InventoryUpgradeButton>().Select(); ; break;
            default: return;
        }
        currentSelectedUpgradeSlot = upgradeSlotType;
    }

    public void SelectWeaponSlot(WeaponSlotType upgradeSlotType) {
        DeselectWeaponSlots();
        DeselectUpgradeSlots();

        inventoryUIVars.InventoryUpgradeListRoot.SetActive(false);
        inventoryUIVars.InventoryWeaponListRoot.SetActive(true);
        invHeader.text = "Weapons";

        switch (upgradeSlotType) {
            case WeaponSlotType.One: inventoryUIVars.WeaponOneGroup.GetComponent<InventoryWeaponButton>().Select(); break;
            case WeaponSlotType.Two: inventoryUIVars.WeaponTwoGroup.GetComponent<InventoryWeaponButton>().Select(); ; break;
            default: return;
        }
        currentSelectedWeaponSlot = upgradeSlotType;
    }
    private void DeselectWeaponSlots() {
        inventoryUIVars.WeaponOneGroup.GetComponent<InventoryWeaponButton>().Deselect();
        inventoryUIVars.WeaponTwoGroup.GetComponent<InventoryWeaponButton>().Deselect();
    }
    private void DeselectUpgradeSlots() {
        inventoryUIVars.UpgradeOneGroup.GetComponent<InventoryUpgradeButton>().Deselect();
        inventoryUIVars.UpgradeTwoGroup.GetComponent<InventoryUpgradeButton>().Deselect();
        inventoryUIVars.UpgradeThreeGroup.GetComponent<InventoryUpgradeButton>().Deselect();
    }


    public void SetupInventoryUI(PlayerInventorySO currentInventory) {
/*        if (currentInventory.weapons.weaponOne)
            inventoryUIVars.WeaponOneGroup.GetComponent<Image>().sprite = currentInventory.weapons.weaponOne.weaponSO.weaponShopData.icon;
        if (currentInventory.weapons.weaponTwo)
            inventoryUIVars.WeaponTwoGroup.GetComponent<Image>().sprite = currentInventory.weapons.weaponTwo.weaponSO.weaponShopData.icon;*/

        inventoryUIVars.UpgradeOneGroup.GetComponent<InventoryUpgradeButton>().SetupButton(currentInventory.upgrades.UpgradeOne);
        inventoryUIVars.UpgradeTwoGroup.GetComponent<InventoryUpgradeButton>().SetupButton(currentInventory.upgrades.UpgradeTwo);
        inventoryUIVars.UpgradeThreeGroup.GetComponent<InventoryUpgradeButton>().SetupButton(currentInventory.upgrades.UpgradeThree);

        inventoryUIVars.WeaponOneGroup.GetComponent<InventoryWeaponButton>().SetupButton(currentInventory.weapons.weaponOne);
        inventoryUIVars.WeaponTwoGroup.GetComponent<InventoryWeaponButton>().SetupButton(currentInventory.weapons.weaponTwo);
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
        Debug.LogWarning($"hp: {(int)healthAmount}");
        this.gameUIVars.healthText.text = ((int)healthAmount).ToString();
    }
}
