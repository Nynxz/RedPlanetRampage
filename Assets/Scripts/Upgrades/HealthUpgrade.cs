using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/UpgradeAbilities/Health")]
// Abstract Class
public class HealthUpgrade : UpgradeAbilitySO {

    public float HealthIncrease; // Flat Point Increase

    public override void Equip() {
        Debug.Log(GameManager.Instance.name);
        GameManager.Instance.PlayerManager.GetPlayer.playerStats.HealthMaximum += HealthIncrease;
        GameManager.Instance.PlayerManager.GetPlayer.TryHeal(HealthIncrease);
    }

    public override void Unequip() { // In Reality.. We Probably Wont Use this as modifiers are created anew each Equip
        GameManager.Instance.PlayerManager.GetPlayer.playerStats.HealthMaximum -= HealthIncrease;
        GameManager.Instance.PlayerManager.GetPlayer.TryHeal(HealthIncrease);
    }
}
