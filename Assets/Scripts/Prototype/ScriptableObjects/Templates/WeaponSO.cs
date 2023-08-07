using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://youtu.be/ALSTea0HXZI
[CreateAssetMenu(menuName = "Data/Weapon")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public float weaponShootCooldown;
    public GameObject weaponPrefab;
    public BulletSO bulletSO;
    public int ammoCount;
    public int magCount;
    public float reloadTime;


}
