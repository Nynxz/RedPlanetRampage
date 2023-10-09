using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgradeInfo : MonoBehaviour {
    [SerializeField] private Image selectedImage;
    [SerializeField] private TextMeshProUGUI NameTMP;
    [SerializeField] private TextMeshProUGUI CostTMP;
    [SerializeField] private TextMeshProUGUI InfoTMP;


    [SerializeField] private TextMeshProUGUI IsUnlockedTMP;

    public void Setup(UpgradeUnlockS u) {
        selectedImage.sprite = u.upgrade.Icon;
        selectedImage.color = u.upgrade.IconTint;
        NameTMP.text = $"Name: {u.upgrade.Name}";
        CostTMP.text = $"Cost: {u.upgrade.Cost}";
        InfoTMP.text = $"Info: {u.upgrade.InfoText}";
    }
}
