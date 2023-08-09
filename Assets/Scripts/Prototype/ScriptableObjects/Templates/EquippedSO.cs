using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedSO : ScriptableObject
{
    public WeaponSO weaponSO;
    private int currentAmmoInMag;
    private int totalAmmoLeft;


    public void Setup() {
        currentAmmoInMag = weaponSO.weaponData.ammoCount;
        totalAmmoLeft = weaponSO.weaponData.ammoCount * weaponSO.weaponData.magCount - currentAmmoInMag;
    }

    public bool CanShoot() {
        if (currentAmmoInMag > 0) {
            return true;
        } else {
            Debug.Log("Out Of Ammo In Mag");
            return false;
        }
    }
    public void Shoot() {
        if (currentAmmoInMag > 0) {
            currentAmmoInMag--;
        } else {
            //Reload();
        }
    }
    public void Reload() {
        if(totalAmmoLeft <= 0) {
            Debug.Log("No Ammo Left");
        }else if (weaponSO.weaponData.ammoCount - currentAmmoInMag <= totalAmmoLeft) {
            totalAmmoLeft -= weaponSO.weaponData.ammoCount - currentAmmoInMag;
            currentAmmoInMag = weaponSO.weaponData.ammoCount;
        } else { // Reload as much as we can
            currentAmmoInMag = totalAmmoLeft;
            totalAmmoLeft = 0;
        }
    }

    public PlayerManager.UpdateAmmoArgs GetAmmoArgs() {
        return new PlayerManager.UpdateAmmoArgs() {
            currentInMag = currentAmmoInMag,
            maximumInMag = weaponSO.weaponData.ammoCount,
            ammoLeft = totalAmmoLeft
        };
    }

    public void FillAmmo() {
        Setup();
    }
}
