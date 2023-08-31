using UnityEngine;
/*
    An implemntation of the interactable class,
    used as a debug for testing
 */
public class MissionSetterDebug : Interactable {

    public MissionSO mission;

    public override void interact(GameObject player) {
        GameManager.Instance.MissionManager.SetToLoadMission(mission);
    }

    public override string onHoverText() {
        return "Set Mission: " + mission.MissionData.Name;
    }
}
