using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform gunRoot;
    [SerializeField] private List<EquippedSO> inventory;

    private EquippedSO currentEquippedWeaponSO;
    private GameObject currentWeaponVisual;
    private Transform muzzleTransform;

    // Testing Weapons
    private float currentShootTimer;
    private bool toShoot = false;

    [SerializeField] private WeaponSO[] testWeapons;

    private int currentWeaponIndex = 0;

    private void Start() {
        foreach(WeaponSO newWeapon in testWeapons) {
            Debug.Log("Equiping " +  newWeapon.name);
            EquipWeapon(newWeapon);
        }
        currentWeaponVisual = null;
    }

    public void EquipWeapon(WeaponSO weapon) {
        EquippedSO newEquip = ScriptableObject.CreateInstance<EquippedSO>();
        newEquip.weaponSO = weapon;
        newEquip.Setup();
        newEquip.name = weapon.weaponData.weaponName;
        inventory.Add(newEquip);
        Debug.Log(inventory.Count);
    }


    void Update()
    {
        if (GameManager.Instance.InputManager.shootInput && currentShootTimer <= 0 && currentWeaponIndex >= 0) {
            Shoot();
        }
        if (currentShootTimer > 0) {
            currentShootTimer -= Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeWeapon(inventory[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeWeapon(inventory[1]);
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
        if(Input.GetKeyDown(KeyCode.R)) {
            toShoot = false;
            currentEquippedWeaponSO.Reload();
            GameManager.Instance.UIManager.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());

        }
    }

    private void ChangeWeapon(EquippedSO newWeapon) {
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
                BulletTest bullet = b.GetComponent<BulletTest>();
                bullet.Setup(gunRoot.forward, currentEquippedWeaponSO.weaponSO.weaponData.bulletSO.bulletSpeed, currentEquippedWeaponSO.weaponSO.weaponData.bulletSO.bulletDamage);
                GameManager.Instance.UIManager.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());
            }
        }
    }

    public void FillAmmo() {
        currentEquippedWeaponSO.FillAmmo();
        GameManager.Instance.UIManager.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());

    }
}
