using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour {

    [SerializeField] protected LayerMask groundLayerMask;
    [SerializeField] protected LayerMask enemyLayerMask;
    protected LayerMask enemyAndGroundLayerMask; // Combination of both

    [SerializeField] protected AudioClip OnShootAudio;
}
