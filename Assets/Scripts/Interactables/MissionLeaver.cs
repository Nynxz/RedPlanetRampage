using System;
using UnityEngine;
/*
    An implemntation of the interactable class,
    used as a debug for testing
 */
public class MissionLeaver : MonoBehaviour, IInteractable {

    public static event Action MissionLeft;

    public void interact(GameObject player) {
        GameManager.Instance.MissionManager.LeaveMission();
        MissionLeft?.Invoke();
    }

    public string onHoverText() {
        return "Exit Mission";
    }
}
