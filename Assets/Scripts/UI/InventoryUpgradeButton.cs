using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpgradeButton : MonoBehaviour {

    public enum UpgradeSlotType {
        One, Two, Three, None
    }

    [SerializeField] private UpgradeSlotType upgradeSlotType;
    [SerializeField] private Button baseButton;
    [SerializeField] private Button removeButton;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image selectImage;
    [SerializeField] private TextMeshProUGUI upgradeText;


    public void SetupButton(UpgradeSO upgradeSO) {

        baseButton.onClick.RemoveAllListeners();
        removeButton.onClick.RemoveAllListeners();

        baseButton.onClick.AddListener(() => {
            GameManager.Instance.UIManager.SelectUpgradeSlot(upgradeSlotType);
            GameManager.Instance.AudioManager.PlaySoundSelect();

        });

        if (upgradeSO == null) {
            ClearSlotVisual();
            return;
        }

        SetSlotVisual(upgradeSO);

        WeaponManager weaponManager = GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>();

        removeButton.onClick.AddListener(() => {
            weaponManager.UnequipSlot(upgradeSlotType);
        });
    }

    private void ClearSlotVisual() {
        iconImage.sprite = null;
        iconImage.color = Color.white;
        upgradeText.text = null;
        removeButton.gameObject.SetActive(false);
    }

    private void SetSlotVisual(UpgradeSO upgradeSO) {
        iconImage.sprite = upgradeSO.Icon;
        iconImage.color = upgradeSO.IconTint;
        upgradeText.text = upgradeSO.name;
        removeButton.gameObject.SetActive(true);
    }

    public void Select() {
        selectImage.gameObject.SetActive(true);
    }

    public void Deselect() {
        selectImage.gameObject.SetActive(false);
    }
}
