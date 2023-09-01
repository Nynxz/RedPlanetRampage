using UnityEngine;
/*
    An implemntation of the interactable class,
    used as a debug for testing
 */
public class MissionLeaver : MonoBehaviour, IInteractable {

    public void interact(GameObject player) {
        GameManager.Instance.MissionManager.LeaveMission();
    }

    public string onHoverText() {
        return "Exit Mission";
    }
}
