using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://youtu.be/ALSTea0HXZI  example of scriptable objects for weapons, there are many

// Used to group data about the weapon
[System.Serializable]
public class WeaponData {
    public string weaponName;
    public float weaponShootCooldown;
    public GameObject weaponPrefab;
    public BulletSO bulletSO;
    public int ammoCount;
    public int magCount;
    public float reloadTime;
}


// Used to group data about the weapon related to the shop
[System.Serializable]
public class WeaponShopData {
    public int cost;
    public string description;
    public string icon;
}


// The raw weapon data, these are not modified at runtime, we put this data into an EquippedSO
[CreateAssetMenu(menuName = "Data/Weapon")]
[System.Serializable]
public class WeaponSO : ScriptableObject
{
    public WeaponData weaponData;
    public WeaponShopData weaponShopData;


}
