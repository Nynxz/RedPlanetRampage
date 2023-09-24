using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static PlayerInventorySO;

public class ShopItemVisualUpgrade : MonoBehaviour, ISelectHandler {
    public UpgradeSO upgradeSO;
    public TextMeshProUGUI UpgradeName;
    public TextMeshProUGUI UpgradeCost;
    public Image UpgradeIcon;
    public int indexInList;

    private Button button;

    protected void Awake() {
        //button = GetComponent<Button>();
        //button.onClick.AddListener(() => { GameManager.Instance.NewShopManager.SelectUpgrade(upgradeSO); });
    }

    public void OnSelect(BaseEventData eventData) {
        Debug.Log("Selecting");
        GameManager.Instance.NewShopManager.SelectUpgrade(indexInList);
    }

    public void Setup(UpgradeUnlockS uS, int index) {
        indexInList = index;
        //weaponSO = wS.weapon;
        UpgradeName.text = uS.upgrade.Name;
        UpgradeCost.text = uS.upgrade.Cost.ToString();
        UpgradeIcon.sprite = uS.upgrade.Icon;
    }
}
