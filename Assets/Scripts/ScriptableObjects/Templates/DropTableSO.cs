using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/DropTable")]
public class DropTableSO : ScriptableObject
{
    [System.Serializable]
    public struct Drop {
        public Item item;
        public int chance;
    }
    public List<Drop> drops;

    private float cumulativeChance = 0f;
    float[] chancesMap;

    public void OnEnable() {
        cumulativeChance = 0f;
        chancesMap = new float[drops.Count];
        for (int i = 0; i < drops.Count; i++) {
            cumulativeChance += drops[i].chance;
            chancesMap[i] = cumulativeChance;
        }
    }

    public Item RandomDrop() {
        float roll = Random.Range(0f, cumulativeChance);
        for (int i = 0; i < drops.Count; i++) {
            if (roll <= chancesMap[i]) {
                return drops[i].item;
            }
        }
        return null;
    }
}
