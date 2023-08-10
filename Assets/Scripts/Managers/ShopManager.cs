using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class ShopUI {
    public TextMeshProUGUI selectedName;
    public TextMeshProUGUI selectedCost;
    public TextMeshProUGUI selectedDamage;
    public TextMeshProUGUI selectedRateOfFire;
    public TextMeshProUGUI selectedAmmo;

    public Button BuyButton;
}

public class ShopManager : MonoBehaviour
{
    public ShopSO startingShopSO;

    [SerializeField] private ShopUI shopUI;

    private ShopSO currentShopSO;
    public List<GameObject> currentItemList;
    private WeaponSO selectedItem;

    public event EventHandler<WeaponSO> SelectItem;



    private void Start() {
        currentItemList = new List<GameObject>();
        currentShopSO = ScriptableObject.CreateInstance<ShopSO>();
        currentShopSO.weaponsForSale = Instantiate(startingShopSO).weaponsForSale;
        RefreshShop();
        SelectItem += ShopManager_SelectItem;
        shopUI.BuyButton.onClick.AddListener(TryBuySelectedItem);
    }

    private void ShopManager_SelectItem(object sender, WeaponSO e) {
        selectedItem = e;
        shopUI.selectedName.text = $"Name: {e.weaponData.weaponName}";
        shopUI.selectedCost.text = $"Cost: {e.weaponShopData.cost}";
        shopUI.selectedDamage.text = $"Damage: {e.weaponData.bulletSO.bulletDamage}";
        string rateOfFireString;
        double cooldown = Math.Floor(1 / e.weaponData.weaponShootCooldown);
        if (cooldown == 0) {
            cooldown = e.weaponData.weaponShootCooldown;
            rateOfFireString = $"Rate of Fire: 1 every {cooldown}s";
        } else {
            rateOfFireString = $"Rate of Fire: {cooldown} per s";
        }
        shopUI.selectedRateOfFire.text = rateOfFireString;
        shopUI.selectedAmmo.text = $"Ammo: {e.weaponData.ammoCount} | ({e.weaponData.magCount})";
    }

    public void SelectWeapon(WeaponSO weapon) {
        SelectItem?.Invoke(this, weapon);
    }

    private void TryBuySelectedItem() {
        Debug.Log("Buying?");
        if(selectedItem != null) {
            GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().EquipWeapon(selectedItem);
        }
    }

    public void AddNewWeapon(WeaponSO newWeapon) {
        if (!currentShopSO.weaponsForSale.Exists(weapon => weapon == newWeapon)) {
            currentShopSO.weaponsForSale.Add(newWeapon);
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
            ShopItemVisual shopItem = Instantiate(uiManager.shopUIVars.ShopOptionPrefab, uiManager.shopUIVars.VerticalShopButtonGroup.transform).GetComponent<ShopItemVisual>();
            currentItemList.Add(shopItem.gameObject);
            shopItem.WeaponName.text = weapon.weaponData.weaponName;
            shopItem.WeaponCost.text = weapon.weaponShopData.cost.ToString();
            shopItem.weaponSO = weapon;
            shopItem.name = weapon.weaponData.weaponName;

        }
    }
}
