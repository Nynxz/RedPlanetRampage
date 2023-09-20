using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerInventorySO;


public class NewShopManager : MonoBehaviour {
    [SerializeField] private PlayerUnlocksSO CurrentUnlocks;

    [SerializeField] private GameObject shopItemPrefab;

    [SerializeField] private Transform weaponOptionsVertical;

    [SerializeField] private ShopWeaponInfo weaponInfo; // Right side of shop

    [SerializeField] private Button BuyButton;

    private GameObject[] weaponVisualItems;

    private int currentSelectedWeaponIndex = -1;

    private void Start() {
    }


    public void LoadShop(PlayerUnlocksSO unlocks) {
        CurrentUnlocks = unlocks;
        weaponVisualItems = new GameObject[CurrentUnlocks.weapons.Count];

        for (int i = 0; i < unlocks.weapons.Count; i++) {
            weaponVisualItems[i] = Instantiate(shopItemPrefab, weaponOptionsVertical);
            ShopItemVisualWeapon sVisW = weaponVisualItems[i].GetComponent<ShopItemVisualWeapon>();
            sVisW.Setup(unlocks.weapons[i], i);
        }

        BuyButton.onClick.AddListener(() => {
            if(currentSelectedWeaponIndex >= 0) {
                TryBuyWeapon(currentSelectedWeaponIndex);
            }
        });
        //CurrentUnlocks.Save();
    }

    public void RefreshShop() {
        for (int i = 0; i < CurrentUnlocks.weapons.Count; i++) {
            ShopItemVisualWeapon sVisW = weaponVisualItems[i].GetComponent<ShopItemVisualWeapon>();
            sVisW.Setup(CurrentUnlocks.weapons[i], i);
        }
        //CurrentUnlocks.Save();
    }

    public void SelectWeapon(int index) {
        weaponInfo.Setup(CurrentUnlocks.weapons[index]);
        currentSelectedWeaponIndex = index;
    }

    public void TryBuyWeapon(int index) {
        // TODO
        // Cost Check
        // Return Visual If Insufficient Money
        Debug.Log("Trying to buy weapon!");
        Debug.Log(CurrentUnlocks.weapons[index].weapon.weaponData.weaponName);
        GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>().AddWeaponToInventory(CurrentUnlocks.weapons[index].weapon);
    }

}
