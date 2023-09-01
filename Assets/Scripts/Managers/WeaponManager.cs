using System;
using System.Collections.Generic;
using UnityEngine;

//baseInventory is just data, it is then instantiated to player inventory... either through a new save or loading an inventory

public class WeaponManager : MonoBehaviour {

    [SerializeField] private Transform gunRoot;
    [SerializeField] private List<EquippedSO> inventory;
    [SerializeField] private PlayerInventorySO baseInventory;
    private PlayerInventorySO playerInventory;
    public PlayerInventorySO PlayerInventorySO => playerInventory;

    private EquippedSO currentEquippedWeaponSO;
    private GameObject currentWeaponVisual;
    private Transform muzzleTransform;

    // Testing Weapons
    private float currentShootTimer;

    public event Action OnShoot; // Invoked when a weapon shoots
    public static event Action<PlayerInventorySO> OnInventoryChanged;
    public event Action OnWeaponChanged;


    private enum CurrentWeaponSlot {
        One, Two, None
    }

    private CurrentWeaponSlot currentWeaponSlot = CurrentWeaponSlot.None;

    protected void Start() {
        SetupBaseInventory();
        currentWeaponVisual = null;
        playerInventory.EquipInventory();
    }

    public void SetupBaseInventory() {
        playerInventory = Instantiate(baseInventory); // Create a copy
    }

    public void LoadInventory(PlayerInventorySO inventory) {
        playerInventory = Instantiate(inventory); // Create a copy
    }

    public void SaveCurrentInventory() {
        Debug.Log(JsonUtility.ToJson(playerInventory, true));
    }

    protected void Update() {

        if (GameManager.Instance.InputManager.shootInput) {
            TryShoot();
        }

        if (currentShootTimer > 0) {
            currentShootTimer -= Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeWeapon(playerInventory.weapons.weaponOne);
            currentWeaponSlot = CurrentWeaponSlot.One;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeWeapon(playerInventory.weapons.weaponTwo);
            currentWeaponSlot = CurrentWeaponSlot.Two;
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            currentWeaponSlot = CurrentWeaponSlot.None;
            UnequipWeapon();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            //toShoot = false;
            currentEquippedWeaponSO.Reload();
            GameManager.Instance.UIManager.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());
            SaveCurrentInventory();
        }
    }


    public void AddWeaponToInventory(WeaponSO weapon) {
        playerInventory.weapons.equippedStorage.Add(CreateEquippedSOFromWeaponSO(weapon));
        OnInventoryChanged?.Invoke(playerInventory);
    }

    public void AddUpgradeToInventory(UpgradeSO upgrade) {
        playerInventory.upgrades.upgradeStorage.Add(upgrade);
        OnInventoryChanged?.Invoke(playerInventory);
    }

    public void UnequipWeapon() {
        Destroy(currentWeaponVisual);
        currentWeaponVisual = null;
        GameManager.Instance.UIManager.SetAmmoText("No Weapon");
        GameManager.Instance.UIManager.SetupInventoryUI(GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>().PlayerInventorySO);
    }
    private void ChangeWeapon(EquippedSO newWeapon) {
        if (newWeapon == null) return;

        Destroy(currentWeaponVisual);
        //toShoot = false;
        currentEquippedWeaponSO = newWeapon;
        currentShootTimer = currentEquippedWeaponSO.weaponSO.weaponData.weaponShootCooldown;
        currentWeaponVisual = Instantiate(currentEquippedWeaponSO.weaponSO.weaponData.weaponPrefab, gunRoot);
        muzzleTransform = currentWeaponVisual.transform.Find("Muzzle");
        GameManager.Instance.UIManager.SetAmmoText(newWeapon.GetAmmoArgs());
    }
    private void TryShoot() {
        // TODO: Check if weapon is equipped
        if (currentEquippedWeaponSO && currentShootTimer <= 0) {
            Shoot();
        }
    }

    private void Shoot() {
        //toShoot = false;

        if (currentWeaponVisual != null) {
            currentShootTimer = currentEquippedWeaponSO.weaponSO.weaponData.weaponShootCooldown;
            if (currentEquippedWeaponSO.CanShoot()) {
                currentEquippedWeaponSO.Shoot();
                GameObject b = Instantiate(currentEquippedWeaponSO.weaponSO.weaponData.bulletSO.bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
                ProjectileBullet bullet = b.GetComponent<ProjectileBullet>();
                bullet.Setup(muzzleTransform.forward, currentEquippedWeaponSO.weaponSO.weaponData.bulletSO.bulletSpeed, currentEquippedWeaponSO.weaponSO.weaponData.bulletSO.bulletDamage);
                GameManager.Instance.UIManager.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());
                OnShoot?.Invoke();
            }
        }
    }

    public void FillAmmo() {
        currentEquippedWeaponSO.FillAmmo();
        GameManager.Instance.UIManager.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());

    }

    private void AddUpgrade(UpgradeSO upgradeSO) {
        if (upgradeSO != null) {
            upgradeSO.EnableAbilities();
        }
    }

    private void DisableUpgrade(UpgradeSO upgradeSO) {
        if (upgradeSO != null) {
            upgradeSO.DisableAbilities();
        }
    }

    public void UnequipSlot(InventoryUpgradeButton.UpgradeSlotType upgradeSlotType) {

        if (upgradeSlotType == InventoryUpgradeButton.UpgradeSlotType.One) {
            playerInventory.upgrades.upgradeStorage.Add(PlayerInventorySO.upgrades.UpgradeOne);
            PlayerInventorySO.upgrades.UpgradeOne.DisableAbilities();
            PlayerInventorySO.upgrades.UpgradeOne = null;
        } else if (upgradeSlotType == InventoryUpgradeButton.UpgradeSlotType.Two) {
            playerInventory.upgrades.upgradeStorage.Add(PlayerInventorySO.upgrades.UpgradeTwo);
            PlayerInventorySO.upgrades.UpgradeTwo.DisableAbilities();
            PlayerInventorySO.upgrades.UpgradeTwo = null;
        } else if (upgradeSlotType == InventoryUpgradeButton.UpgradeSlotType.Three) {
            playerInventory.upgrades.upgradeStorage.Add(PlayerInventorySO.upgrades.UpgradeThree);
            PlayerInventorySO.upgrades.UpgradeThree.DisableAbilities();
            PlayerInventorySO.upgrades.UpgradeThree = null;
        }

        OnInventoryChanged?.Invoke(playerInventory);
    }

    public void SwapSlotWithUpgradeIndex(InventoryUpgradeButton.UpgradeSlotType upgradeSlotType, int upgradeIndex) {
        switch (upgradeSlotType) {
            case InventoryUpgradeButton.UpgradeSlotType.One: TrySwapUpgrade(ref PlayerInventorySO.upgrades.UpgradeOne, upgradeIndex); break;
            case InventoryUpgradeButton.UpgradeSlotType.Two: TrySwapUpgrade(ref PlayerInventorySO.upgrades.UpgradeTwo, upgradeIndex); break;
            case InventoryUpgradeButton.UpgradeSlotType.Three: TrySwapUpgrade(ref PlayerInventorySO.upgrades.UpgradeThree, upgradeIndex); break;
        }
    }

    public void SwapSlotWithWeaponIndex(InventoryWeaponButton.WeaponSlotType upgradeSlotType, int weaponIndex) {
        switch (upgradeSlotType) {
            case InventoryWeaponButton.WeaponSlotType.One: TrySwapWeapon(ref PlayerInventorySO.weapons.weaponOne, weaponIndex); break;
            case InventoryWeaponButton.WeaponSlotType.Two: TrySwapWeapon(ref PlayerInventorySO.weapons.weaponTwo, weaponIndex);  break;
        }
        if(upgradeSlotType == InventoryWeaponButton.WeaponSlotType.One && currentWeaponSlot == CurrentWeaponSlot.One) {
            ChangeWeapon(playerInventory.weapons.weaponOne);
        }
        else if (upgradeSlotType == InventoryWeaponButton.WeaponSlotType.Two && currentWeaponSlot == CurrentWeaponSlot.Two) {
            ChangeWeapon(playerInventory.weapons.weaponTwo);
        }
    }

    private void TrySwapUpgrade(ref UpgradeSO oldUpgrade, int upgradeIndex) {
        UpgradeSO toAdd = playerInventory.upgrades.upgradeStorage[upgradeIndex];
        if (oldUpgrade) {
            playerInventory.upgrades.upgradeStorage[upgradeIndex] = oldUpgrade;
        } else { // else just remove it from the list
            playerInventory.upgrades.upgradeStorage.RemoveAt(upgradeIndex);
        }
        DisableUpgrade(oldUpgrade); // Disable Currently Equipped
        oldUpgrade = toAdd; // Set currently equipped to new upgrade
        AddUpgrade(toAdd);
        OnInventoryChanged?.Invoke(playerInventory);
    }

    private void TrySwapWeapon(ref EquippedSO oldUpgrade, int upgradeIndex) {
        EquippedSO toAdd = playerInventory.weapons.equippedStorage[upgradeIndex];
        if (oldUpgrade) {
            playerInventory.weapons.equippedStorage[upgradeIndex] = oldUpgrade;
        } else { // else just remove it from the list
            playerInventory.weapons.equippedStorage.RemoveAt(upgradeIndex);
        }
        // todo: unequip/ requip
        oldUpgrade = toAdd; // Set currently equipped to new upgrade
        //UnequipWeapon();
        OnInventoryChanged?.Invoke(playerInventory);
    }

    public static EquippedSO CreateEquippedSOFromWeaponSO(WeaponSO weapon) {
        EquippedSO equippedSO = ScriptableObject.CreateInstance<EquippedSO>();
        equippedSO.weaponSO = weapon;
        equippedSO.Setup();
        equippedSO.name = weapon.weaponData.weaponName;
        return equippedSO;
    }
}
