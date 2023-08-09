using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://youtu.be/ALSTea0HXZI

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

[System.Serializable]
public class WeaponShopData {
    public int cost;
    public string description;
    public string icon;
}


[CreateAssetMenu(menuName = "Data/Weapon")]
[System.Serializable]
public class WeaponSO : ScriptableObject
{
    public WeaponData weaponData;
    public WeaponShopData weaponShopData;


}
