using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgradeInfo : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI NameTMP;
    [SerializeField] private TextMeshProUGUI CostTMP;


    [SerializeField] private TextMeshProUGUI IsUnlockedTMP;

    public void Setup(UpgradeUnlockS u) {
        NameTMP.text = $"Name: {u.upgrade.Name}";
        CostTMP.text = $"Cost: {u.upgrade.Cost}";
    }
}
