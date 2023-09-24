using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerInventorySO;


public class NewShopManager : MonoBehaviour {
    [SerializeField] private PlayerUnlocksSO CurrentUnlocks;

    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameObject shopItemPrefabUpgrade;

    [SerializeField] private Transform weaponOptionsVertical;
    [SerializeField] private Transform upgradeOptionsVertical;

    [SerializeField] private ShopWeaponInfo weaponInfo; // Right side of shop
    [SerializeField] private ShopUpgradeInfo upgradeInfo; // Right side of shop

    [SerializeField] private Button BuyButton;

    private GameObject[] weaponVisualItems;
    private GameObject[] upgradeVisualItems;

    private int currentSelectedWeaponIndex = -1;
    private int currentSelectedUpgradeIndex = -1;

    private void Start() {
    }


    public void LoadShop(PlayerUnlocksSO unlocks) {
        CurrentUnlocks = unlocks;
        weaponVisualItems = new GameObject[CurrentUnlocks.weapons.Count];
        upgradeVisualItems = new GameObject[CurrentUnlocks.upgrades.Count];

        for (int i = 0; i < unlocks.weapons.Count; i++) {
            weaponVisualItems[i] = Instantiate(shopItemPrefab, weaponOptionsVertical);
            ShopItemVisualWeapon sVisW = weaponVisualItems[i].GetComponent<ShopItemVisualWeapon>();
            sVisW.Setup(unlocks.weapons[i], i);
        }

        for (int i = 0; i < unlocks.upgrades.Count; i++) {
            upgradeVisualItems[i] = Instantiate(shopItemPrefabUpgrade, upgradeOptionsVertical);
            ShopItemVisualUpgrade sVisU = upgradeVisualItems[i].GetComponent<ShopItemVisualUpgrade>();
            sVisU.Setup(unlocks.upgrades[i], i);
        }

        BuyButton.onClick.AddListener(() => {
            if(currentSelectedWeaponIndex >= 0) {
                TryBuyWeapon(currentSelectedWeaponIndex);
            }
            if (currentSelectedUpgradeIndex >= 0) {
                TryBuyUpgrade(currentSelectedUpgradeIndex);
            }
        });
        //CurrentUnlocks.Save();
    }

    public void RefreshShop() {
        for (int i = 0; i < CurrentUnlocks.weapons.Count; i++) {
            ShopItemVisualWeapon sVisW = weaponVisualItems[i].GetComponent<ShopItemVisualWeapon>();
            sVisW.Setup(CurrentUnlocks.weapons[i], i);
        }
        for(int i = 0; i < CurrentUnlocks.upgrades.Count; i++) {
            ShopItemVisualUpgrade sVisU = upgradeVisualItems[i].GetComponent<ShopItemVisualUpgrade>();
            sVisU.Setup(CurrentUnlocks.upgrades[i], i);
        }
        //CurrentUnlocks.Save();
    }

    public void SelectWeapon(int index) {
        weaponInfo.Setup(CurrentUnlocks.weapons[index]);
        currentSelectedUpgradeIndex = -1;
        currentSelectedWeaponIndex = index;
    }

    public void SelectUpgrade(int index) {
        upgradeInfo.Setup(CurrentUnlocks.upgrades[index]);
        currentSelectedUpgradeIndex = index;
        currentSelectedWeaponIndex = -1;
    }

    public void TryBuyWeapon(int index) {
        // TODO
        // Cost Check
        // Return Visual If Insufficient Money
        Debug.Log("Trying to buy weapon!");
        Debug.Log(CurrentUnlocks.weapons[index].weapon.weaponData.weaponName);
        GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>().AddWeaponToInventory(CurrentUnlocks.weapons[index].weapon);
    }
    public void TryBuyUpgrade(int index) {
        // TODO
        // Cost Check
        // Return Visual If Insufficient Money
        Debug.Log("Trying to buy weapon!");
        Debug.Log(CurrentUnlocks.upgrades[index].upgrade.Name);
        GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>().AddUpgradeToInventory(CurrentUnlocks.upgrades[index].upgrade);
    }
}
