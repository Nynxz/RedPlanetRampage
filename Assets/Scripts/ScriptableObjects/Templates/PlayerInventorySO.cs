using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/PlayerInventory")]
public class PlayerInventorySO : ScriptableObject {

    [Serializable]
    public struct Weapons {
        public EquippedSO weaponOne;
        public EquippedSO weaponTwo;
    }
    [Serializable]
    public struct Upgrades {
        public string UpgradeOne;
        public string UpgradeTwo;
        public string UpgradeThree;
    }

    [SerializeField] public Weapons weapons;
    [SerializeField] public Upgrades upgrades;


    protected void OnEnable() {
        if(EditorApplication.isPlaying) return;
        weapons.weaponOne.Setup();
        weapons.weaponTwo.Setup();
    }
}
