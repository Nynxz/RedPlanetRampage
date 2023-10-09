using UnityEngine;

public class HealthPickup : Item {
    [SerializeField] private float healAmount;
    protected override bool TryOnPickup(Player player) {
        GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>().FillAmmo();
        player.TryHeal(healAmount);
        return true;
    }

}
