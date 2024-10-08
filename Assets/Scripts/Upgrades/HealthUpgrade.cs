using UnityEngine;

[CreateAssetMenu(menuName = "Data/UpgradeAbilities/Health")]
// Abstract Class
public class HealthUpgrade : UpgradeAbilitySO {

    public float HealthIncrease; // Flat Point Increase

    public override void Equip() {
        GameManager.Instance.PlayerManager.Player.PlayerStats.HealthMaximum += HealthIncrease;
        GameManager.Instance.PlayerManager.Player.TryHeal(HealthIncrease);
    }

    public override void Unequip() { // In Reality.. We Probably Wont Use this as modifiers are created anew each Equip
        GameManager.Instance.PlayerManager.Player.PlayerStats.HealthMaximum -= HealthIncrease;
        GameManager.Instance.PlayerManager.Player.TryHeal(HealthIncrease);
    }
}
