using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveZombieSpawner : MonoBehaviour {

    public static event Action<WaveChangeEventArgs> WaveChangeEvent;
    public static event Action AllWavesCompletedEvent;

    public class WaveChangeEventArgs : EventArgs {
        public int currentWave;
        public int killedZombiesThisWave;
        public int totalZombiesThisWave;
    }

    [SerializeField] private List<ZombieWaveSO> zombieWaves;
    [SerializeField] private Transform[] zombieSpawns;

    private bool wavesStarted = false;
    private int currentWave = 0;
    private int killedZombiesThisWave = 0;

    private WaveChangeEventArgs waveChangeEventArgs;
    //Max Waves
    private float spawnTimerMax = 1f;
    private float spawnTimerCurrent = 0f;

    void OnEnable() {
        //StartWaves();
        zombieWaves.ForEach((wave) => { wave.Setup(); });

        currentWave = 1;

        waveChangeEventArgs = new WaveChangeEventArgs() {
            currentWave = currentWave,
            killedZombiesThisWave = 0,
            totalZombiesThisWave = 0
        };

        waveChangeEventArgs.totalZombiesThisWave = zombieWaves[currentWave - 1].totalZombies;
        waveChangeEventArgs.currentWave = currentWave;
        waveChangeEventArgs.killedZombiesThisWave = 0;

    }

    private void Start() {
        WaveChangeEvent?.Invoke(waveChangeEventArgs);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.H) || spawnTimerCurrent >= spawnTimerMax) {
            SpawnZombie();
            spawnTimerCurrent = 0f;
        }
        spawnTimerCurrent += Time.deltaTime;
    }

    private void SpawnZombie() {
        if (currentWave <= zombieWaves.Count) {
            int spawnIdx = UnityEngine.Random.Range(0, zombieSpawns.Length);
            bool didSpawn = zombieWaves[currentWave - 1].SpawnRandomZombie(zombieSpawns[spawnIdx].position, out Zombie zombie);
            if (zombie) {
                zombie.ThisZombieDied += Zombie_ZombieDied;
            }
            if (!didSpawn) {
                if(killedZombiesThisWave == zombieWaves[currentWave - 1].totalZombies) {
                    currentWave++;
                    if(currentWave > zombieWaves.Count) {
                        // Invoke Game Finished
                        Debug.Log("Game Finished");
                        AllWavesCompletedEvent?.Invoke();
                    } else {
                        killedZombiesThisWave = 0;

                        waveChangeEventArgs.totalZombiesThisWave = zombieWaves[currentWave - 1].totalZombies;
                        waveChangeEventArgs.currentWave = currentWave;
                        waveChangeEventArgs.killedZombiesThisWave = 0;
                        WaveChangeEvent?.Invoke(waveChangeEventArgs);
                    }
                }
            }
        }
    }

    private void Zombie_ZombieDied() {
        killedZombiesThisWave++;
        waveChangeEventArgs.killedZombiesThisWave++;
        WaveChangeEvent?.Invoke(waveChangeEventArgs);
    }

    public void StartWaves() {
        currentWave = 1;
        if(zombieWaves.Count <= currentWave) {
            if (!zombieWaves[currentWave - 1].SpawnRandomZombie(GameManager.Instance.PlayerManager.PlayerPosition, out _)) {
                currentWave++;
            };
        }
    }
}
