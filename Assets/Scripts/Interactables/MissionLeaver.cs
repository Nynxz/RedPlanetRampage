using UnityEngine;
/*
    An implemntation of the interactable class,
    used as a debug for testing
 */
public class MissionLeaver : Interactable {

    public override void interact(GameObject player) {
        GameManager.Instance.MissionManager.LeaveMission();
    }

    public override string onHoverText() {
        return "Exit Mission";
    }
}
