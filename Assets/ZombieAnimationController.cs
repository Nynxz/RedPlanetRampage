using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    public bool isStanding = false;

    public void IsAwake() {
        isStanding = true;
    }
}
