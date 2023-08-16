using UnityEngine;

public class MoneyPickup : Item {
    [SerializeField] private int moneyAmount;
    protected override bool TryOnPickup(Player player) {
        GameManager.Instance.PlayerManager.AddMoney(moneyAmount);
        return true;
    }

}
