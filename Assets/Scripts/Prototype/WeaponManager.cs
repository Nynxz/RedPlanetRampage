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



    private void Start() {
        foreach(WeaponSO newWeapon in testWeapons) {
            Debug.Log("Equiping " +  newWeapon.name);
            EquipWeapon(newWeapon);
        }
        currentWeaponVisual = null;
    }

    private void EquipWeapon(WeaponSO weapon) {
        EquippedSO newEquip = ScriptableObject.CreateInstance<EquippedSO>();
        newEquip.weaponSO = weapon;
        newEquip.Setup();
        inventory.Add(newEquip);
        Debug.Log(inventory.Count);
    }


    void Update()
    {
        if (Input.GetMouseButton(0) && currentShootTimer <= 0) {
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
            ChangeWeapon(inventory[2]);
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            Destroy(currentWeaponVisual);
            currentWeaponVisual = null;
            GameManager.Instance.SetAmmoText("No Weapon");
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            toShoot = false;
            currentEquippedWeaponSO.Reload();
            GameManager.Instance.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());

        }
    }

    private void ChangeWeapon(EquippedSO newWeapon) {
        Destroy(currentWeaponVisual);
        toShoot = false;
        currentEquippedWeaponSO = newWeapon;
        currentShootTimer = currentEquippedWeaponSO.weaponSO.weaponShootCooldown;
        currentWeaponVisual = Instantiate(currentEquippedWeaponSO.weaponSO.weaponPrefab, gunRoot);
        muzzleTransform = currentWeaponVisual.transform.Find("Muzzle");
        GameManager.Instance.SetAmmoText(newWeapon.GetAmmoArgs());
    }

    private void Shoot() {
        toShoot = false;
        
        if (currentWeaponVisual != null) {
            currentShootTimer = currentEquippedWeaponSO.weaponSO.weaponShootCooldown;
            if (currentEquippedWeaponSO.CanShoot()) {
                currentEquippedWeaponSO.Shoot();
                GameObject b = Instantiate(currentEquippedWeaponSO.weaponSO.bulletSO.bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
                BulletTest bullet = b.GetComponent<BulletTest>();
                bullet.Setup(gunRoot.forward, currentEquippedWeaponSO.weaponSO.bulletSO.bulletSpeed, currentEquippedWeaponSO.weaponSO.bulletSO.bulletDamage);
                GameManager.Instance.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());
            }
        }
    }

    public void FillAmmo() {
        currentEquippedWeaponSO.FillAmmo();
        GameManager.Instance.SetAmmoText(currentEquippedWeaponSO.GetAmmoArgs());

    }
}
