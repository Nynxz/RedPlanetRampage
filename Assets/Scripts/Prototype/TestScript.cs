using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UpdateMoney += OnUpdateMoney;    
    }

    private void OnUpdateMoney(object sender, GameManager.UpdateMoneyEventArgs e) {
        Debug.Log("New Money :D");
    }

}
