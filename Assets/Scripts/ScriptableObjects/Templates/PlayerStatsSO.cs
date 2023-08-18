using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerStats")]
public class PlayerStatsSO : ScriptableObject {

    [Header("Health")]
    public float HealthMaximum;

    [Header("Movement")]
    public float MoveSpeedModifier;
    public float SprintSpeedModifier;
    public float SprintDurationModifier;
    public float SprintRecoveryModifier;

    [Header("Weapons")]
    public float ReloadSpeedModifier;
    public float ExtraMagazines;

    [Header("Resistances")]
    public float FireResistance;

}
