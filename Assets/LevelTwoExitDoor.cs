using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoExitDoor : MonoBehaviour {
    // Start is called before the first frame update
    void Start()
    {
        WaveZombieSpawner.AllWavesCompletedEvent += WaveZombieSpawner_AllWavesCompletedEvent;
    }

    private void WaveZombieSpawner_AllWavesCompletedEvent() {
        // Open the door
        gameObject.SetActive(false);
        WaveZombieSpawner.AllWavesCompletedEvent -= WaveZombieSpawner_AllWavesCompletedEvent;
    }

}
