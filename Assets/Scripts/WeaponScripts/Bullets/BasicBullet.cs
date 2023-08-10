using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet {
    [SerializeField] private bool doDifferent;


    public override void OnEnable() {
        base.OnEnable();
        if(doDifferent) {
            DoGroundHit = () => {
                Debug.Log("jks different");
            };
        }
    }
}
