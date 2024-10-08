using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeaponInfo : MonoBehaviour {
    [SerializeField] private Image SelectedImage;
    [SerializeField] private TextMeshProUGUI NameTMP;
    [SerializeField] private TextMeshProUGUI CostTMP;
    [SerializeField] private TextMeshProUGUI DamageTMP;


    [SerializeField] private TextMeshProUGUI IsUnlockedTMP;

    public void Setup(WeaponUnlockS u) {
        SelectedImage.sprite = u.weapon.weaponShopData.icon;
        NameTMP.text = $"Name: {u.weapon.weaponData.weaponName}";
        CostTMP.text = $"Cost: {u.weapon.weaponShopData.cost}";
        DamageTMP.text = $"Damage: {u.weapon.weaponData.bulletSO.bulletDamage}";
    }
}
