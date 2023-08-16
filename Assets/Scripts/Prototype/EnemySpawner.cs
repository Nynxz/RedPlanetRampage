using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float spawnDelay;

    [SerializeField] private bool enabled;
    private float currentDelay;

    private void Start() {
        currentDelay = spawnDelay;
    }
    private void Update() {
        if (!enabled) return;
        currentDelay -= Time.deltaTime;
        if (currentDelay <= 0) {
            currentDelay = spawnDelay;
            Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count - 1)]);
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        foreach (Transform t in spawnPoints) {
            Gizmos.DrawSphere(t.position, 0.5f);
        }
    }
}
