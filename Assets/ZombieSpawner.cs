using System.Collections.Generic;
using UnityEngine;

// Handles zombie spawns
// list of zombie spawn nodes
// types of zombies to spawn etc
// Zombies 'come out' of the ground, to give players time to react

public class ZombieSpawner : MonoBehaviour {

    [SerializeField] private int maximumLiveZombieCount;
    [SerializeField] private int maximumTotalZombieCount;
    [SerializeField] private int minimumSpawnTimeDelay;

    [SerializeField] private List<Transform> spawnpoints;
    [SerializeField] private List<GameObject> zombieTypes;


    private float currentDelay = 0f;
    private int currentTotalZombieCount = 0;

    void Update() {
        if(currentDelay >= minimumSpawnTimeDelay && currentTotalZombieCount < maximumTotalZombieCount) {
            currentDelay = 0f;
            int pointToSpawnAt = Random.Range(0, spawnpoints.Count);
            SpawnZombie(spawnpoints[pointToSpawnAt].position);
        }
        currentDelay += Time.deltaTime;
    }

    private void SpawnZombie(Vector3 spawnPosition) {
        int zombieToSpawn = Random.Range(0, zombieTypes.Count);
        Instantiate(zombieTypes[zombieToSpawn], spawnPosition, Quaternion.identity);
    }
}