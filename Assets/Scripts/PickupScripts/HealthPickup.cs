using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Item
{
    [SerializeField] private float healAmount;
    protected override bool TryOnPickup(Player player) {
        if(player.TryHeal(healAmount)) {
            itemSO.PlayPickupSound();
            return true;
        }
        return player.TryHeal(healAmount);
    }

}
