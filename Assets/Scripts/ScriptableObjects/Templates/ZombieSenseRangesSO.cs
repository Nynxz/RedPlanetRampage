using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/ZombieVariables")]
public class ZombieVariables : ScriptableObject
{
    public float attackDamage;

    [Header("Ranges")]
    public float awakeRange;
    public float chaseRange;
    public float attackRange;

    [Header("Cooldowns")]
    public float actionCooldown;
    public float attackCooldown;

}
