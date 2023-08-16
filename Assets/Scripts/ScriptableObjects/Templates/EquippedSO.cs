using UnityEngine;

// A scriptable object for converting the weaponSO "Data" into a weapon the player actually uses
// We do not create instances manually of this usually, as these are generated at runtime,
// eg: when the player buys a weapon
// each EquippedSO has its own ammo currently, this allows you to have two of the same weapon with different ammo counts
public class EquippedSO : ScriptableObject {
    public WeaponSO weaponSO;
    private int currentAmmoInMag;
    private int totalAmmoLeft;

    // Gets the data from weaponSO and sets the instance to the correct information
    public void Setup() {
        currentAmmoInMag = weaponSO.weaponData.ammoCount;
        totalAmmoLeft = weaponSO.weaponData.ammoCount * weaponSO.weaponData.magCount - currentAmmoInMag;
    }

    // Checks if there is enough ammo to shoot
    public bool CanShoot() {
        if (currentAmmoInMag > 0) {
            return true;
        } else {
            Debug.Log("Out Of Ammo In Mag");
            return false;
        }
    }

    // Take ammo away from the mag
    public void Shoot() {
        if (currentAmmoInMag > 0) {
            currentAmmoInMag--;
        } else {
            //Reload();
        }
    }

    // Reload the mag, taking ammo from ammoCount if available 
    public void Reload() {
        if (totalAmmoLeft <= 0) {
            Debug.Log("No Ammo Left");
        } else if (weaponSO.weaponData.ammoCount - currentAmmoInMag <= totalAmmoLeft) {
            totalAmmoLeft -= weaponSO.weaponData.ammoCount - currentAmmoInMag;
            currentAmmoInMag = weaponSO.weaponData.ammoCount;
        } else { // Reload as much as we can
            currentAmmoInMag = totalAmmoLeft;
            totalAmmoLeft = 0;
        }
    }

    // Retruns structured information
    public PlayerManager.UpdateAmmoArgs GetAmmoArgs() {
        return new PlayerManager.UpdateAmmoArgs() {
            currentInMag = currentAmmoInMag,
            maximumInMag = weaponSO.weaponData.ammoCount,
            ammoLeft = totalAmmoLeft
        };
    }

    // Wrapper for filling ammo, TODO: change or remove
    public void FillAmmo() {
        Setup();
    }
}
