using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/PlayerInventory")]
public class PlayerInventorySO : ScriptableObject {

    [Serializable]
    public struct Weapons {
        public EquippedSO weaponOne;
        public EquippedSO weaponTwo;
        public List<EquippedSO> equippedStorage;
    }
    [Serializable]
    public struct Upgrades {
        public UpgradeSO UpgradeOne;
        public UpgradeSO UpgradeTwo;
        public UpgradeSO UpgradeThree;
        public List<UpgradeSO> upgradeStorage;
    }

    [SerializeField] public Weapons weapons;
    [SerializeField] public Upgrades upgrades;


    public void EquipInventory() {
        //if(EditorApplication.isPlaying) return;
        if (weapons.weaponOne)
            weapons.weaponOne.Setup();
        if (weapons.weaponTwo)
            weapons.weaponTwo.Setup();
        Debug.Log("Hello World!!");

        if (upgrades.UpgradeOne)
            upgrades.UpgradeOne.EnableAbilities();
        if (upgrades.UpgradeTwo)
            upgrades.UpgradeTwo.EnableAbilities();
        if (upgrades.UpgradeThree)
            upgrades.UpgradeThree.EnableAbilities();
    }

}
