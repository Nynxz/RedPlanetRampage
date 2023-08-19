using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/UpgradeAbilities/Speed")]
// Abstract Class
public class SpeedUpgrade : UpgradeAbilitySO {

    public enum SpeedType {
        Move,
        Sprint
    }

    public SpeedType speedType;

    [Min(1)]
    public float ModifierAmount; // 1.2 = 20% Increase // 1 = No Increase

    public override void Equip() {
        if(speedType == SpeedType.Move) {
            GameManager.Instance.PlayerManager.GetPlayer.playerStats.MoveSpeedModifier *= ModifierAmount;
        } else if ( speedType == SpeedType.Sprint) {
            GameManager.Instance.PlayerManager.GetPlayer.playerStats.SprintSpeedModifier *= ModifierAmount;
        }
    }

    public override void Unequip() { // In Reality.. We Probably Wont Use this as modifiers are created anew each Equip
        if (speedType == SpeedType.Move) {
            GameManager.Instance.PlayerManager.GetPlayer.playerStats.MoveSpeedModifier /= ModifierAmount;
        } else if (speedType == SpeedType.Sprint) {
            GameManager.Instance.PlayerManager.GetPlayer.playerStats.SprintSpeedModifier /= ModifierAmount;
        }
    }
}
