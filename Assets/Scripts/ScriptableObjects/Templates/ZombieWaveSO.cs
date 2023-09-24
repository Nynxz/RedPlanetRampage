using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/ZombieWave")]
public class ZombieWaveSO : ScriptableObject {


    [Serializable]
    public struct ZombieCount {
        public GameObject zombie;
        public int count;
        private int currentCount;

        public void Reset() {
            currentCount = 0;
        }

        public Zombie Spawn(Vector3 pos) {
            if(currentCount < count) {
                currentCount++;
                return Instantiate(zombie, pos, Quaternion.identity).GetComponent<Zombie>();
            }
            return null;
        }
    }

    // Zombie Types to Spawn This Wave
    [SerializeField] private List<ZombieCount> zombiesToSpawn;
    public List<int> zombieIndexLeft;
    public int totalZombies = 0;

    public void Setup() {
        zombieIndexLeft = new List<int>();
        zombiesToSpawn.ForEach((zombie) => { zombie.Reset(); }); // Reset Zombie Counts - editor breaks SO's
        for(int i = 0; i < zombiesToSpawn.Count; i++) {
            for(int x = 0; x < zombiesToSpawn[i].count; x++) {
                zombieIndexLeft.Add(i);
            }
        }
        totalZombies = zombieIndexLeft.Count;
        Debug.Log(zombieIndexLeft.ToArray());
    }

    public bool SpawnRandomZombie(Vector3 pos, out Zombie zombie) {
        if(zombieIndexLeft.Count == 0) { zombie = null; return false; }
        int idx = UnityEngine.Random.Range(0, zombieIndexLeft.Count);
        int zIdx = zombieIndexLeft[idx];
        zombie = zombiesToSpawn[zIdx].Spawn(pos);
        zombieIndexLeft.RemoveAt(idx);
        Debug.Log(zombieIndexLeft.ToArray());

        if(zombieIndexLeft.Count == 0) {
            return false;
        } else {
            return true;
        }
    }
}
