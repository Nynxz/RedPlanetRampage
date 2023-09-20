using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages the players progress towards collecting the 4 different ship parts to escape

public class ShipPartManager : MonoBehaviour {
    public enum ShipPart { First, Second, Third, Fourth };

    [Serializable]
    private struct ShipPartInfo {
        public ShipPart Part;
        public string Name;
        public bool isCollected;
        public bool isDeposited;
    }

    [SerializeField] private ShipPartInfo[] ShipParts = new ShipPartInfo[4];

    // 4 different ship parts
    // can be 'collected' and deposited
    // 1 in each level

    public void CollectPart(ShipPart shipPart) {
        ShipParts[(int)shipPart].isCollected = true;
    }

    public void TryDepositParts() {
        bool allDeposited = true;
        for(int i = 0; i < ShipParts.Length; i++) {
            if (ShipParts[i].isCollected) {
                ShipParts[i].isDeposited = true;
            }
            if (!ShipParts[i].isDeposited) {
                allDeposited = false;
            }
        }

        if(allDeposited) {
            Debug.Log("Game Complete");
        }
    }
}
