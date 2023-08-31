using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[System.Serializable]
public class RewardSO : ScriptableObject, IRewardable {

    
    [System.Serializable]
    public struct SRewardData {
        public string Name;
        public string Description;
    }

    public SRewardData RewardData;

    public virtual void RewardPlayer() { //IRewardable
        throw new System.NotImplementedException();
    }
}
