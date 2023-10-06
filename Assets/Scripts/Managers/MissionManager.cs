using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour {
    public enum MissionStatus { None, ToLoad, Loaded, Completed, Failed };

    public static event Action<bool> MissionSet;

    public MissionSO[] UnlockedMissions;
    public MissionSO CurrentMission;
    public MissionStatus CurrentMissionStatus = MissionStatus.None;


    private void Start() {
        if(UnlockedMissions.Length > 0) {
            SetToLoadMission(UnlockedMissions[0]);
        }
    }

    public void LoadSetMission() {
        if (CurrentMission == null) return;
        MissionSet?.Invoke(false); // We have left a mission, therefore the mission is no longer set
        SceneManager.LoadScene(CurrentMission.MissionData.BaseSceneName);
        CurrentMissionStatus = MissionStatus.Loaded;
    }

    public void LeaveMission() {
        SceneManager.LoadScene("Hub-H");
        CurrentMissionStatus = MissionStatus.None;
        CurrentMission = null;
    }

    public void UnlockNewMission(MissionSO mission) {
        if(!UnlockedMissions.Contains(mission)) { 
            UnlockedMissions.Append(mission);
        }
    }

    public void SetToLoadMission(MissionSO mission) {
        if (CurrentMission == mission) {
            // Trying to load already loaded mission, clear loaded mission
            CurrentMission = null;
            CurrentMissionStatus = MissionStatus.None;
            MissionSet?.Invoke(false);
            return;
        };

        CurrentMission = mission;
        CurrentMissionStatus = MissionStatus.ToLoad;
        MissionSet?.Invoke(true);
    }
}
