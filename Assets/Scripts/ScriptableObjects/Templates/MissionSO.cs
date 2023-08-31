using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




[CreateAssetMenu(menuName ="Data/Mission")]
[System.Serializable]
public class MissionSO : RewardSO {

    [System.Serializable]
    public struct SMissionData {
        public string Name;
        public string Description;
        public SceneAsset BaseScene;
    }

    public SMissionData MissionData;
    public RewardSO[] Rewards;
    // TODO: public ISpendable[] Costs;
    // Objectives?
    // Failures?

    public override void RewardPlayer() { //IRewardable
        throw new System.NotImplementedException();
    }

    public void LoadMission() {

    }
}
