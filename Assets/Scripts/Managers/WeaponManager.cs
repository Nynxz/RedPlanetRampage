using System;
using System.Collections.Generic;
using UnityEngine;



public class WeaponManager : MonoBehaviour {

    [SerializeField] private Transform gunRoot;
    [SerializeField] private List<EquippedSO> inventory;
    [SerializeField] private PlayerInventorySO playerInventory;
    public PlayerInventorySO PlayerInventorySO => playerInventory;

    private EquippedSO currentEquippedWeaponSO;
    private GameObject currentWeaponVisual;
    private Transform muzzleTransform;

    // Testing Weapons
    private float currentShootTimer;

    public event Action OnShoot; // Invoked when a weapon shoots
    public static event Action<PlayerInventorySO> OnUpgradeChanged;
    public event Action OnWeaponChanged;


    protected void Start() {
        currentWeaponVisual = null;
        playerInventory.EquipInventory();
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
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeWeapon(playerInventory.weapons.weaponTwo);
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            Destroy(currentWeaponVisual);
            currentWeaponVisual = null;
            GameManager.Instance.UIManager.SetAmmoText("No Weapon");
            GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>().playerInventory.UnequipU1();
            GameManager.Instance.UIManager.SetupInventoryUI(GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>().PlayerInventorySO);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            //toShoot = false;
            currentEquippedWeaponSO.Reload();
            GameManager.Instance.UIManager.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());

        }
    }
 

    public void AddWeaponToInventory(WeaponSO weapon) {
        inventory.Add(CreateEquippedSOFromWeaponSO(weapon));
        Debug.Log(inventory.Count);
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
        if (currentShootTimer <= 0) {
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

        OnUpgradeChanged?.Invoke(playerInventory);
    }

    public void SwapSlotWithUpgradeIndex(InventoryUpgradeButton.UpgradeSlotType upgradeSlotType, int upgradeIndex) {
        if (upgradeSlotType == InventoryUpgradeButton.UpgradeSlotType.One) {
            TrySwap(ref PlayerInventorySO.upgrades.UpgradeOne, upgradeIndex);
        } else if (upgradeSlotType == InventoryUpgradeButton.UpgradeSlotType.Two) {
            TrySwap(ref PlayerInventorySO.upgrades.UpgradeTwo, upgradeIndex);
        } else if (upgradeSlotType == InventoryUpgradeButton.UpgradeSlotType.Three) {
            TrySwap(ref PlayerInventorySO.upgrades.UpgradeThree, upgradeIndex);
        }
    }

    private void TrySwap(ref UpgradeSO oldUpgrade, int upgradeIndex) {
        UpgradeSO toAdd = playerInventory.upgrades.upgradeStorage[upgradeIndex];
        if (oldUpgrade) {
            playerInventory.upgrades.upgradeStorage[upgradeIndex] = oldUpgrade;
        } else { // else just remove it from the list
            playerInventory.upgrades.upgradeStorage.RemoveAt(upgradeIndex);
        }
        DisableUpgrade(oldUpgrade); // Disable Currently Equipped
        oldUpgrade = toAdd; // Set currently equipped to new upgrade
        AddUpgrade(toAdd);
        OnUpgradeChanged?.Invoke(playerInventory);
    }

    public static EquippedSO CreateEquippedSOFromWeaponSO(WeaponSO weapon) {
        EquippedSO equippedSO = ScriptableObject.CreateInstance<EquippedSO>();
        equippedSO.weaponSO = weapon;
        equippedSO.Setup();
        equippedSO.name = weapon.weaponData.weaponName;
        return equippedSO;
    }
}
