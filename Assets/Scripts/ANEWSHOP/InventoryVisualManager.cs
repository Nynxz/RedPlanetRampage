using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryVisualManager : MonoBehaviour {
    [SerializeField] private GameObject InventoryGroup;


    [SerializeField] private GameObject InventoryUpgradeUIPrefab;
    [SerializeField] private GameObject InventoryWeaponUIPrefab;

    [SerializeField] private GameObject weaponButtonGroup;
    [SerializeField] private GameObject upgradeButtonGroup;
    [Header("Weapon & Upgrade Slots")]
    [SerializeField] private GameObject WeaponOneGroup;
    [SerializeField] private GameObject WeaponTwoGroup;
    [SerializeField] private GameObject UpgradeOneGroup;
    [SerializeField] private GameObject UpgradeTwoGroup;
    [SerializeField] private GameObject UpgradeThreeGroup;

    private GameManager gameManager;

    protected void Start() {
        gameManager = GameManager.Instance;
        NewUIManager.EmitInventoryToggle += HandleInventoryToggleEvent;
        WeaponManager.OnInventoryChanged += WeaponManager_OnInventoryChanged;

    }

    private void WeaponManager_OnInventoryChanged(PlayerInventorySO currentInventory) {
        foreach (Transform listItem in upgradeButtonGroup.transform) {
            Destroy(listItem.gameObject);
        }
        for (int i = 0; i < currentInventory.upgrades.upgradeStorage.Count; i++) {
            UpgradeUIOption uiOption = Instantiate(InventoryUpgradeUIPrefab, upgradeButtonGroup.transform).GetComponent<UpgradeUIOption>();
            uiOption.Setup(currentInventory.upgrades.upgradeStorage[i], i);
        }

        foreach (Transform listItem in weaponButtonGroup.transform) {
            Destroy(listItem.gameObject);
        }
        for (int i = 0; i < currentInventory.weapons.equippedStorage.Count; i++) {
            WeaponUIOption uiOption = Instantiate(InventoryWeaponUIPrefab, weaponButtonGroup.transform).GetComponent<WeaponUIOption>();
            uiOption.Setup(currentInventory.weapons.equippedStorage[i], i);
        }
        RefreshInventoryUI(currentInventory);

    }

    private void HandleInventoryToggleEvent() {
        if (!InventoryGroup.activeInHierarchy) {
            GameManager.Instance.InputManager.DisableInput();
            Cursor.visible = true;
            InventoryGroup.SetActive(true);
        } else {
            GameManager.Instance.InputManager.EnableInput();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            InventoryGroup.SetActive(false);
        }
    }

    private void RefreshInventoryUI(PlayerInventorySO currentInventory) {
        UpgradeOneGroup.GetComponent<InventoryUpgradeButton>().SetupButton(currentInventory.upgrades.UpgradeOne);
        UpgradeTwoGroup.GetComponent<InventoryUpgradeButton>().SetupButton(currentInventory.upgrades.UpgradeTwo);
        UpgradeThreeGroup.GetComponent<InventoryUpgradeButton>().SetupButton(currentInventory.upgrades.UpgradeThree);

        WeaponOneGroup.GetComponent<InventoryWeaponButton>().SetupButton(currentInventory.weapons.weaponOne);
        WeaponTwoGroup.GetComponent<InventoryWeaponButton>().SetupButton(currentInventory.weapons.weaponTwo);
    }

    private void RefreshInventory() {
        // Put the weapon buttons in the list
        // which buttons?
    }
}
