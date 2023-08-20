using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopUI {
    public TextMeshProUGUI selectedNameWeapon;
    public TextMeshProUGUI selectedCostWeapon;
    public TextMeshProUGUI selectedDamageWeapon;
    public TextMeshProUGUI selectedRateOfFireWeapon;
    public TextMeshProUGUI selectedAmmoWeapon;

    public TextMeshProUGUI selectedNameUpgrade;
    public TextMeshProUGUI selectedCostUpgrade;
    public TextMeshProUGUI selectedStatsUpgrade;
    public Image selectedStatsIcon;

    public GameObject rightSideWeaponGroup;
    public GameObject rightSideUpgradeGroup;

    public Button BuyButtonWeapon;
    public Button BuyButtonUpgrade;
}

public class ShopManager : MonoBehaviour {
    public ShopSO startingShopSO;

    [SerializeField] private ShopUI shopUI;

    private ShopSO currentShopSO;
    public List<GameObject> currentItemList;
    private WeaponSO selectedItemWeapon;
    private UpgradeSO selectedItemUpgrade;

    public event EventHandler<WeaponSO> OnSelectWeapon;
    public event EventHandler<UpgradeSO> OnSelectUpgrade;

    public enum CurrentShop {
        Upgrades, Weapons
    }
    private CurrentShop currentShop;

    protected void Start() {
        currentItemList = new List<GameObject>();
        currentShopSO = ScriptableObject.CreateInstance<ShopSO>();
        currentShopSO.weaponsForSale = Instantiate(startingShopSO).weaponsForSale;
        currentShopSO.upgradesForSale = Instantiate(startingShopSO).upgradesForSale;
        RefreshShop();
        OnSelectWeapon += ShopManager_SelectWeapon;
        OnSelectUpgrade += ShopManager_SelectUpgrade;
        shopUI.BuyButtonWeapon.onClick.AddListener(TryBuySelectedItem);
        shopUI.BuyButtonUpgrade.onClick.AddListener(TryBuySelectedItem);
    }

    private void ShopManager_SelectWeapon(object sender, WeaponSO e) {
        selectedItemWeapon = e;
        shopUI.selectedNameWeapon.text = $"Name: {e.weaponData.weaponName}";
        shopUI.selectedCostWeapon.text = $"Cost: {e.weaponShopData.cost}";
        shopUI.selectedDamageWeapon.text = $"Damage: {e.weaponData.bulletSO.bulletDamage}";
        string rateOfFireString;
        double cooldown = Math.Floor(1 / e.weaponData.weaponShootCooldown);
        if (cooldown == 0) {
            cooldown = e.weaponData.weaponShootCooldown;
            rateOfFireString = $"Rate of Fire: 1 every {cooldown}s";
        } else {
            rateOfFireString = $"Rate of Fire: {cooldown} per s";
        }
        shopUI.selectedRateOfFireWeapon.text = rateOfFireString;
        shopUI.selectedAmmoWeapon.text = $"Ammo: {e.weaponData.ammoCount} | ({e.weaponData.magCount})";
    }

    private void ShopManager_SelectUpgrade(object sender, UpgradeSO e) {
        selectedItemUpgrade = e;
        shopUI.selectedNameUpgrade.text = $"Name: {e.Name}";
        shopUI.selectedCostUpgrade.text = $"Cost: {e.Cost}";
        shopUI.selectedStatsUpgrade.text = $"Stats: TODO";
        shopUI.selectedStatsIcon.sprite = e.Icon;
        shopUI.selectedStatsIcon.color = e.IconTint;

    }

    public void SelectWeapon(WeaponSO weapon) {
        OnSelectWeapon?.Invoke(this, weapon);
    }

    public void SelectUpgrade(UpgradeSO upgrade) {
        OnSelectUpgrade?.Invoke(this, upgrade);
    }

    private void TryBuySelectedItem() {
        Debug.Log("Buying?");
        switch (currentShop) {
            case CurrentShop.Weapons: {
                    if (selectedItemWeapon != null) {
                        Debug.Log("Trying to add weapon to inventory");
                        GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>().AddWeaponToInventory(selectedItemWeapon);
                    }
                    break; 
            }
            case CurrentShop.Upgrades: {
                    if (selectedItemUpgrade != null) {
                        Debug.Log("Trying to add upgrade to inventory");
                        GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>().AddUpgradeToInventory(selectedItemUpgrade);
                    }
                    break; 
            }
        }

    }

    public void AddNewWeapon(WeaponSO newWeapon) {
        if (!currentShopSO.weaponsForSale.Exists(weapon => weapon == newWeapon)) {
            currentShopSO.weaponsForSale.Add(newWeapon);
            RefreshShop();
        };
    }

    public void AddNewUpgrade(UpgradeSO newUpgrade) {
        if (!currentShopSO.upgradesForSale.Exists(weapon => weapon == newUpgrade)) {
            currentShopSO.upgradesForSale.Add(newUpgrade);
            RefreshShop();
        };
    }

    private void RefreshShop() {
        UIManager uiManager = GameManager.Instance.UIManager;
        foreach (GameObject item in currentItemList) {
            Destroy(item);
        }
        currentItemList.Clear();

        foreach (WeaponSO weapon in currentShopSO.weaponsForSale) {
            Debug.Log(weapon.weaponData.weaponName + " " + weapon.weaponShopData.cost);
            ShopItemVisualWeapon shopItem = Instantiate(uiManager.shopUIVars.ShopOptionPrefabWeapon, uiManager.shopUIVars.VerticalShopButtonGroupWeapons.GetComponentInChildren<VerticalLayoutGroup>().transform).GetComponent<ShopItemVisualWeapon>();
            currentItemList.Add(shopItem.gameObject);
            shopItem.WeaponName.text = weapon.weaponData.weaponName;
            shopItem.WeaponCost.text = weapon.weaponShopData.cost.ToString();
            shopItem.weaponSO = weapon;
            shopItem.name = weapon.weaponData.weaponName;

        }

        foreach (UpgradeSO upgrade in currentShopSO.upgradesForSale) {
            Debug.Log(upgrade.name + " " + upgrade.Cost);
            ShopItemVisualUpgrade shopItem = Instantiate(uiManager.shopUIVars.ShopOptionPrefabUpgrade, uiManager.shopUIVars.VerticalShopButtonGroupUpgrades.GetComponentInChildren<VerticalLayoutGroup>().transform).GetComponent<ShopItemVisualUpgrade>();
            currentItemList.Add(shopItem.gameObject);
            shopItem.UpgradeName.text = upgrade.Name;
            shopItem.UpgradeCost.text = upgrade.Cost.ToString();
            shopItem.UpgradeIcon.sprite = upgrade.Icon;
            shopItem.UpgradeIcon.color = upgrade.IconTint;
            shopItem.upgradeSO = upgrade;
            shopItem.name = upgrade.Name;

        }
    }

    public CurrentShop SwitchBuyMenus() {

        UIManager uiManager = GameManager.Instance.UIManager;

        if (currentShop == CurrentShop.Weapons) {
            currentShop = CurrentShop.Upgrades;
            shopUI.rightSideWeaponGroup.SetActive(false);
            shopUI.rightSideUpgradeGroup.SetActive(true);
            uiManager.shopUIVars.VerticalShopButtonGroupWeapons.gameObject.SetActive(false);
            uiManager.shopUIVars.VerticalShopButtonGroupUpgrades.gameObject.SetActive(true);
        } else {
            currentShop = CurrentShop.Weapons;
            shopUI.rightSideWeaponGroup.SetActive(true);
            shopUI.rightSideUpgradeGroup.SetActive(false);
            uiManager.shopUIVars.VerticalShopButtonGroupWeapons.gameObject.SetActive(true);
            uiManager.shopUIVars.VerticalShopButtonGroupUpgrades.gameObject.SetActive(false);
        }
        Debug.Log(currentShop);
        return currentShop;
    }
}
