using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Upgrade")]
public abstract class UpgradeAbilitySO : ScriptableObject, IUpgrade {

    public string UpgradeName;

    public abstract void Equip();

    public abstract void Unequip();
}
