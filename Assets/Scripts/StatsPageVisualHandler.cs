using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPageVisualHandler : MonoBehaviour {
    //Hardcoded for ease
    [SerializeField] private TextMeshProUGUI p1TMP;
    [SerializeField] private TextMeshProUGUI p2TMP;
    [SerializeField] private TextMeshProUGUI p3TMP;
    [SerializeField] private TextMeshProUGUI p4TMP;

    [SerializeField] private TextMeshProUGUI zkillCountTMP;
    private int currentKillCount = 0;

    void Start() {
        Debug.Log("Hello");
        ShipPartManager.ShipPartsUpdatedEvent += ShipPartManager_ShipPartsUpdatedEvent;
        PlayerManager.InfoUpdatedEvent += PlayerManager_InfoUpdatedEvent;
        gameObject.SetActive(false);
    }


    private void PlayerManager_InfoUpdatedEvent(PlayerManager.InfoUpdatedEventArgs obj) {
        Debug.Log("Hello");
        zkillCountTMP.text = $"{obj.zombieKills}";
    }

    private void ShipPartManager_ShipPartsUpdatedEvent(ShipPartManager.ShipPartsUpdatedEventArgs obj) {
        p1TMP.text = $"Part 1: {obj.ShipParts[0].isDeposited}";
        p2TMP.text = $"Part 2: {obj.ShipParts[1].isDeposited}";
        p3TMP.text = $"Part 3: {obj.ShipParts[2].isDeposited}";
        p4TMP.text = $"Part 4: {obj.ShipParts[3].isDeposited}";
    }
}
