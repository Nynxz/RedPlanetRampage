using UnityEngine;
/*
    An implemntation of the interactable class,
    used as a debug for testing
 */
public class MissionSetterDebug : MonoBehaviour, IInteractable {

    public MissionSO mission;

    public void interact(GameObject player) {
        GameManager.Instance.MissionManager.SetToLoadMission(mission);
    }

    public string onHoverText() {
        return "Set Mission: " + mission.MissionData.Name;
    }
}
