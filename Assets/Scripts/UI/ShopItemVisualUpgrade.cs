using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerInventorySO;

public class ShopItemVisualUpgrade : MonoBehaviour {
    public UpgradeSO upgradeSO;
    public TextMeshProUGUI UpgradeName;
    public TextMeshProUGUI UpgradeCost;
    public Image UpgradeIcon;

    private Button button;

    protected void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { GameManager.Instance.ShopManager.SelectUpgrade(upgradeSO); });
    }
}
