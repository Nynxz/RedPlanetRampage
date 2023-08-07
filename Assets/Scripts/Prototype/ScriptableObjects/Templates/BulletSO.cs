using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Bullet")]
public class BulletSO : ScriptableObject {

    public string bulletName;
    public int bulletDamage;
    public float bulletSpeed;
    public GameObject bulletPrefab;

}
