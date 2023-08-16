using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemVisual : MonoBehaviour {
    public WeaponSO weaponSO;
    public TextMeshProUGUI WeaponName;
    public TextMeshProUGUI WeaponCost;

    private Button button;

    protected void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { GameManager.Instance.ShopManager.SelectWeapon(weaponSO); });
    }
}
