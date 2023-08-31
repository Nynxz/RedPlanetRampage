using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




[CreateAssetMenu(menuName ="Data/Rewards/Cash")]
[System.Serializable]
public class CashRewardSO : RewardSO {


    public int CashAmount;

    public override void RewardPlayer() { //IRewardable
        GameManager.Instance.PlayerManager.AddMoney(CashAmount);
    }
}
