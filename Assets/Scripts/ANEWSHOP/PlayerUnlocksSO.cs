using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StartingUnlocks")]
public class PlayerUnlocksSO : ScriptableObject {

    public List<WeaponUnlockS> weapons;
    public List<UpgradeUnlockS> upgrades;

    // Player can see all the weapons, click on all weapons and see how to unlock them
    // Base Weapon
    // How to unlock
    // Button to unlock

    public void Save() {
        UnlockSaveData saveData = new UnlockSaveData();
        saveData.weaponUnlocks = new bool[weapons.Count];
        for(int i = 0; i < weapons.Count; i++) {
            saveData.weaponUnlocks[i] = weapons[i].isUnlocked;
        }
        saveData.upgradeUnlocks = new bool[upgrades.Count];
        for (int i = 0; i < upgrades.Count; i++) {
            saveData.upgradeUnlocks[i] = weapons[i].isUnlocked;
        }
        Debug.Log(JsonUtility.ToJson(saveData));
    }

    public void Load(string json) {
        UnlockSaveData sData = JsonUtility.FromJson<UnlockSaveData>(json);
        for (int i = 0; i < sData.weaponUnlocks.Length; i++) {
            weapons[i].isUnlocked = sData.weaponUnlocks[i];
        }
    }

    public void UnlockWeapon(WeaponSO weaponSO) {
        foreach(WeaponUnlockS weaponUnlock in weapons) {
            if(weaponUnlock.weapon ==  weaponSO) {
                weaponUnlock.isUnlocked = true;
            }
        }
    }

    private class UnlockSaveData {
        public bool[] weaponUnlocks;
        public bool[] upgradeUnlocks;
    }
}

[System.Serializable]
public class WeaponUnlockS {
    public WeaponSO weapon;
    public bool isUnlocked;
}

[System.Serializable]
public class UpgradeUnlockS {
    public UpgradeSO upgrade;
    public bool isUnlocked;
}