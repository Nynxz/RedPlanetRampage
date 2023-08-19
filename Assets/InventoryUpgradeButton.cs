using System.Collections;
using System.Collections.Generic;
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
            GameManager.Instance.UIManager.SelectUpgrade(upgradeSlotType);
        });

        if (upgradeSO == null) {
            iconImage.sprite = null;
            iconImage.color = Color.white;
            upgradeText.text = null;
            removeButton.gameObject.SetActive(false);
            return;
        }
        iconImage.sprite = upgradeSO.Icon;
        iconImage.color = upgradeSO.IconTint;
        upgradeText.text = upgradeSO.name;
        removeButton.gameObject.SetActive(true);


        WeaponManager weaponManager = GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>();

        removeButton.onClick.AddListener(() => {
            weaponManager.RemoveUpgrade(ref upgradeSO);

            if (upgradeSlotType == UpgradeSlotType.One) {
                weaponManager.PlayerInventorySO.upgrades.UpgradeOne = null; 
            } else if(upgradeSlotType==UpgradeSlotType.Two) {
                weaponManager.PlayerInventorySO.upgrades.UpgradeTwo = null;
            } else if( upgradeSlotType==UpgradeSlotType.Three) {
                weaponManager.PlayerInventorySO.upgrades.UpgradeThree = null;
            }


            GameManager.Instance.UIManager.SetupInventoryUI(GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().PlayerInventorySO);
            GameManager.Instance.UIManager.SetupInventoryUpgradeList();

            SetupButton(upgradeSO);

        });



    }

    public void Select() {
        selectImage.gameObject.SetActive(true);
    }

    public void Deselect() {
        selectImage.gameObject.SetActive(false);
    }
}
