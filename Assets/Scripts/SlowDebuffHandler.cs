using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDebuffHandler : MonoBehaviour
{

    void Start()
    {
        Player.IsSlowedEvent += Player_IsSlowedEvent;    
        gameObject.SetActive(false);
    }

    private void Player_IsSlowedEvent(bool isSlowed) {
        gameObject.SetActive(isSlowed);
    }
}
