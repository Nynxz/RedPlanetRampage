using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveZombieVisualManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentWave;
    [SerializeField] private TextMeshProUGUI killedZombies;
    [SerializeField] private TextMeshProUGUI totalZombies;


    private void OnEnable() {
        WaveZombieSpawner.WaveChangeEvent += WaveZombieSpawner_WaveChangeEvent;
    }

    private void WaveZombieSpawner_WaveChangeEvent(WaveZombieSpawner.WaveChangeEventArgs obj) {
        currentWave.text = $"Wave: {obj.currentWave}/5";
        killedZombies.text = $"Zombies Left In Wave: {obj.totalZombiesThisWave-obj.killedZombiesThisWave}";
    }
}
