using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterZombie : Zombie {
    [SerializeField] private GameObject smallZombies;
    [SerializeField] private int amount = 3;
    [SerializeField] private float spawnRadius;
    public override void Die() {
        Vector3 deathPos = gameObject.transform.position;
        base.Die();
        for(int i = 0; i < amount; i++) {
            Vector2 posToPlace = Random.insideUnitCircle * spawnRadius;
            Instantiate(smallZombies, deathPos + new Vector3(posToPlace.x, 0, posToPlace.y), Quaternion.identity);
        }
    }
}
