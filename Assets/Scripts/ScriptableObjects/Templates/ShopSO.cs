using System.Collections.Generic;
using UnityEngine;


// Used for the 'starting' of a shop,
// can be serialized for save data for current unlocked shops
// Used currently to clone to a runetime instance which is the one to get weaponUnlocks
// Similar to how EquippedSO works with WeaponSO, we dont want to modify the original data, unless we want to
[CreateAssetMenu(menuName = "Data/Shop")]
public class ShopSO : ScriptableObject {

    public List<WeaponSO> weaponsForSale;
    public List<UpgradeSO> upgradesForSale;

}
