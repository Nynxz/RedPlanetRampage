using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUpgrade {
    public void Equip();
    public void Unequip();
}

// Abstract Class
[CreateAssetMenu(menuName = "Data/Upgrade")]
public class UpgradeSO: ScriptableObject {
    public string Name;
    public float Cost;
    public List<UpgradeAbilitySO> Upgrades;
    public Sprite Icon;
    public Color IconTint;

    public void EnableAbilities() {
        foreach (UpgradeAbilitySO upgradeAbility in Upgrades) {
            upgradeAbility.Equip();
        }
    }

    public void DisableAbilities() {
        foreach (UpgradeAbilitySO upgradeAbility in Upgrades) {
            upgradeAbility.Unequip();
        }
    }
}
