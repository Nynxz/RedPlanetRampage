using System;
using System.Collections.Generic;
using UnityEngine;



public class WeaponManager : MonoBehaviour {



    [SerializeField] private Transform gunRoot;
    [SerializeField] private List<EquippedSO> inventory;
    [SerializeField] private PlayerInventorySO playerInventory;

    private EquippedSO currentEquippedWeaponSO;
    private GameObject currentWeaponVisual;
    private Transform muzzleTransform;

    // Testing Weapons
    private float currentShootTimer;
    private bool toShoot = false;

    [SerializeField] private WeaponSO[] testWeapons;

    private int currentWeaponIndex = 0;

    protected void Start() {
        foreach (WeaponSO newWeapon in testWeapons) {
            Debug.Log("Equiping " + newWeapon.name);
            AddWeaponToInventory(newWeapon);
        }
        currentWeaponVisual = null;


        GameManager.Instance.InputManager.OnShoot += TryShoot;
    }

    private void TryShoot() {
        // TODO: Check if weapon is equipped
        if(currentShootTimer <= 0) {
            Shoot();
        }
    }

    public void AddWeaponToInventory(WeaponSO weapon) {
        inventory.Add(CreateEquippedSOFromWeaponSO(weapon));
        Debug.Log(inventory.Count);
    }


    protected void Update() {

        if (currentShootTimer > 0) {
            currentShootTimer -= Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeWeapon(playerInventory.weapons.weaponOne);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeWeapon(playerInventory.weapons.weaponTwo);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            currentWeaponIndex++;
            if (currentWeaponIndex >= inventory.Count) {
                currentWeaponIndex = 0;
            }
            ChangeWeapon(inventory[currentWeaponIndex]);
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            Destroy(currentWeaponVisual);
            currentWeaponVisual = null;
            GameManager.Instance.UIManager.SetAmmoText("No Weapon");
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            toShoot = false;
            currentEquippedWeaponSO.Reload();
            GameManager.Instance.UIManager.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());

        }
    }

    private void ChangeWeapon(EquippedSO newWeapon) {
        if (newWeapon == null) return;
        
        Destroy(currentWeaponVisual);
        toShoot = false;
        currentEquippedWeaponSO = newWeapon;
        currentShootTimer = currentEquippedWeaponSO.weaponSO.weaponData.weaponShootCooldown;
        currentWeaponVisual = Instantiate(currentEquippedWeaponSO.weaponSO.weaponData.weaponPrefab, gunRoot);
        muzzleTransform = currentWeaponVisual.transform.Find("Muzzle");
        GameManager.Instance.UIManager.SetAmmoText(newWeapon.GetAmmoArgs());
    }

    private void Shoot() {
        toShoot = false;

        if (currentWeaponVisual != null) {
            currentShootTimer = currentEquippedWeaponSO.weaponSO.weaponData.weaponShootCooldown;
            if (currentEquippedWeaponSO.CanShoot()) {
                currentEquippedWeaponSO.Shoot();
                GameObject b = Instantiate(currentEquippedWeaponSO.weaponSO.weaponData.bulletSO.bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
                ProjectileBullet bullet = b.GetComponent<ProjectileBullet>();
                bullet.Setup(muzzleTransform.forward, currentEquippedWeaponSO.weaponSO.weaponData.bulletSO.bulletSpeed, currentEquippedWeaponSO.weaponSO.weaponData.bulletSO.bulletDamage);
                GameManager.Instance.UIManager.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());
            }
        }
    }

    public void FillAmmo() {
        currentEquippedWeaponSO.FillAmmo();
        GameManager.Instance.UIManager.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());

    }


    public static EquippedSO CreateEquippedSOFromWeaponSO(WeaponSO weapon) {
        EquippedSO equippedSO = ScriptableObject.CreateInstance<EquippedSO>();
        equippedSO.weaponSO = weapon;
        equippedSO.Setup();
        equippedSO.name = weapon.weaponData.weaponName;
        return equippedSO;
    }
}
