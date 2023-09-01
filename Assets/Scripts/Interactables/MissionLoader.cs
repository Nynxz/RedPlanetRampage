using UnityEngine;
/*
    An implemntation of the interactable class,
    used as a debug for testing
 */
public class MissionLoader : MonoBehaviour, IInteractable {

    public Light doorLight;
    public MeshRenderer lightObject;

    public Material LightOnMat;
    public Material LightOffMat;

    private void Awake() {
        LightOff();
        MissionManager.MissionSet += MissionManager_MissionSet;
    }
    private void OnDestroy() {
        MissionManager.MissionSet -= MissionManager_MissionSet;

    }

    private void LightOff() {
        doorLight.color = Color.white;
        doorLight.intensity = 0.2f;
        lightObject.material = LightOffMat;
    }
    private void LightOn() {
        doorLight.color = Color.red;
        doorLight.intensity = 2.0f;
        lightObject.material = LightOnMat;
    }

    private void MissionManager_MissionSet(bool obj) {
        if (obj) {
            LightOn();
        } else {
            LightOff();
        }
    }

    public void interact(GameObject player) {
        GameManager.Instance.MissionManager.LoadSetMission();
    }

    public string onHoverText() {
        if(GameManager.Instance.MissionManager.CurrentMissionStatus == MissionManager.MissionStatus.ToLoad) {
            return "Load " + GameManager.Instance.MissionManager.CurrentMission.MissionData.Name;
        }
        return "No Mission Loaded";
    }
}
