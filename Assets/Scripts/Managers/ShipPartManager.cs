using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages the players progress towards collecting the 4 different ship parts to escape

public class ShipPartManager : MonoBehaviour {
    public static event Action GameCompletedEvent;

    public static event Action<ShipPartsUpdatedEventArgs> ShipPartsUpdatedEvent;

    public class ShipPartsUpdatedEventArgs : EventArgs {
        public ShipPartInfo[] ShipParts;
        public ShipPartsUpdatedEventArgs(ShipPartInfo[] shipParts) {
            ShipParts = shipParts;
        }
    }

    public enum ShipPart { First, Second, Third, Fourth };

    [Serializable]
    public struct ShipPartInfo {
        public ShipPart Part;
        public string Name;
        public bool isCollected;
        public bool isDeposited;
    }

    [SerializeField] private ShipPartInfo[] ShipParts = new ShipPartInfo[4];
    private Stack<int> PartsNotSafeForRestart = new Stack<int>();

    // 4 different ship parts
    // can be 'collected' and deposited
    // 1 in each level
    private void Start() {
        MissionLeaver.MissionLeft += MissionLeaver_MissionLeft;
        Player.PlayerDiedEvent += Player_PlayerDiedEvent;
    }

    public void Player_PlayerDiedEvent() {
        // Player has died, clear the parts they collected that level, theyre bad so they dont deserve them.
        foreach (int part in PartsNotSafeForRestart) {
            ShipParts[part].isCollected = false;
        }
    }

    private void MissionLeaver_MissionLeft() {
        PartsNotSafeForRestart.Clear(); // Mission was left properly, clear parts
    }

    public void CollectPart(ShipPart shipPart) {
        ShipParts[(int)shipPart].isCollected = true;
        PartsNotSafeForRestart.Push((int)shipPart);
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
            GameCompletedEvent?.Invoke();
        }

        ShipPartsUpdatedEvent?.Invoke(new ShipPartsUpdatedEventArgs(ShipParts));
    }
}
