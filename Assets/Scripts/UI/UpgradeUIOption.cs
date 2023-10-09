using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIOption : MonoBehaviour {
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI upgradeName;
    [SerializeField] private TextMeshProUGUI upgradeInfo;

    private int indexInList;

    public void Setup(UpgradeSO upgradeSO, int indexInList) {
        this.indexInList = indexInList;
        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        iconImage.sprite = upgradeSO.Icon;
        iconImage.color = upgradeSO.IconTint;
        upgradeName.text = upgradeSO.name;
        upgradeInfo.text = upgradeSO.InfoText;

        gameObject.GetComponent<Button>().onClick.AddListener(() => {

            if (GameManager.Instance.UIManager.currentSelectedUpgradeSlot != InventoryUpgradeButton.UpgradeSlotType.None) { // If a slot is selected

                WeaponManager weaponManager = GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>();

                weaponManager.SwapSlotWithUpgradeIndex(GameManager.Instance.UIManager.currentSelectedUpgradeSlot, this.indexInList);


                GameManager.Instance.AudioManager.PlaySoundSelect();
            }

        });
    }
}
