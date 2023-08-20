using UnityEngine;

public class TestScript : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {
        PlayerManager.UpdateMoney += OnUpdateMoney;
    }

    private void OnUpdateMoney(object sender, PlayerManager.UpdateMoneyEventArgs e) {
        Debug.Log("New Money :D");
    }

}
