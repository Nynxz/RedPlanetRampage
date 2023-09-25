using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static InventoryUpgradeButton;
using static InventoryWeaponButton;


public class NewUIManager : MonoBehaviour {

    public static event Action<bool> EmitShopToggle;
    public static event Action EmitInventoryToggle;

    private GameManager gameManager;
    [SerializeField] private ShopVisualManager shopVisualManager;
    [SerializeField] private DeathScreenVisualManager DeathScreen;

    protected void Start() {
        gameManager = GetComponent<GameManager>();

        InputManager.OptionsPressed += EmitInventoryToggle;
        Player.PlayerDiedEvent += Player_PlayerDiedEvent;
    }

    private void Player_PlayerDiedEvent() {
        DeathScreen.OpenDeathScreen();
        GameManager.DisableGame();
    }

    public static void EmitShopTryOpen() { // Called from interactables
        Debug.Log("trying to open shop");

        InputManager.OptionsPressed -= EmitInventoryToggle;
        InputManager.OptionsPressed += EmitShopTryClose;

        EmitShopToggle?.Invoke(true);
    }

    public static void EmitShopTryClose() {
        Debug.Log("trying to close shop");

        InputManager.OptionsPressed -= EmitShopTryClose;
        InputManager.OptionsPressed += EmitInventoryToggle;

        EmitShopToggle?.Invoke(false);
    }

    public static void EmitInventoryTryOpen() {
        Debug.Log("trying to open inventory");
        EmitInventoryToggle?.Invoke();
    }
}
