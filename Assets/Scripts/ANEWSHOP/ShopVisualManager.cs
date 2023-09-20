using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopVisualManager : MonoBehaviour {
    [SerializeField] private GameObject ShopGroup;
    [SerializeField] private Button shopCloseButton;
    [SerializeField] private Button shopSwitchTypeButton;

    [SerializeField] private GameObject weaponInfoGroup;
    [SerializeField] private GameObject upgradeInfoGroup;

    [SerializeField] private GameObject weaponButtonGroup;
    [SerializeField] private GameObject upgradeButtonGroup;


    private GameManager gameManager;


    private enum CurrentShop {
        Upgrades, Weapons
    }
    private CurrentShop currentShop = CurrentShop.Weapons;


    protected void Start() {
        gameManager = GameManager.Instance;
        NewUIManager.EmitShopToggle += HandleShopEvent;

        shopCloseButton.onClick.AddListener(NewUIManager.EmitShopTryClose);

        shopSwitchTypeButton.onClick.AddListener(() => {
            if (currentShop != CurrentShop.Weapons) 
                SwitchShop(CurrentShop.Weapons);
            else 
                SwitchShop(CurrentShop.Upgrades);
        });

    }


    private void HandleShopEvent(bool toOpen) {
        if (toOpen) {
            GameManager.Instance.NewShopManager.RefreshShop();
            GameManager.Instance.InputManager.DisableInput();
            Cursor.visible = true;
            ShopGroup.SetActive(true);
        } else {
            CloseShop();
        }
    }


    private void CloseShop() {
        GameManager.Instance.InputManager.EnableInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ShopGroup.SetActive(false);
    }
    
    private void SwitchShop(CurrentShop changeToShop) {
        currentShop = changeToShop;
        switch (currentShop) {
            case CurrentShop.Weapons:
                weaponInfoGroup.SetActive(true);
                weaponButtonGroup.SetActive(true);

                upgradeInfoGroup.SetActive(false);
                upgradeButtonGroup.SetActive(false);
                break;
            case CurrentShop.Upgrades:
                weaponInfoGroup.SetActive(false);
                weaponButtonGroup.SetActive(false);

                upgradeInfoGroup.SetActive(true);
                upgradeButtonGroup.SetActive(true);
                break;

        }
    }
}
