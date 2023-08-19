using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIOption : MonoBehaviour {
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI upgradeName;
    [SerializeField] private TextMeshProUGUI upgradeInfo;

    public void Setup(UpgradeSO upgradeSO) {
        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        iconImage.sprite = upgradeSO.Icon;
        iconImage.color = upgradeSO.IconTint;
        upgradeName.text = upgradeSO.name;
        upgradeInfo.text = "TODO";

        gameObject.GetComponent<Button>().onClick.AddListener(() => {

            if (GameManager.Instance.UIManager.currentSelectedUpgradeSlot != InventoryUpgradeButton.UpgradeSlotType.None) {

                WeaponManager weaponManager = GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>();

                // Swap Selected Upgrade With clicked upgrade

                switch (GameManager.Instance.UIManager.currentSelectedUpgradeSlot) {
                    case InventoryUpgradeButton.UpgradeSlotType.One: {
                            if (weaponManager.PlayerInventorySO.upgrades.UpgradeOne) {
                                GameManager.Instance.UIManager.RemoveUpgrade(ref weaponManager.PlayerInventorySO.upgrades.UpgradeOne);
                                weaponManager.PlayerInventorySO.upgrades.upgradeStorage.Add(weaponManager.PlayerInventorySO.upgrades.UpgradeOne);
                            }
                            weaponManager.PlayerInventorySO.upgrades.UpgradeOne = upgradeSO;
                            break;
                        };
                    case InventoryUpgradeButton.UpgradeSlotType.Two: {
                            if (weaponManager.PlayerInventorySO.upgrades.UpgradeTwo) { 
                                GameManager.Instance.UIManager.RemoveUpgrade(ref weaponManager.PlayerInventorySO.upgrades.UpgradeTwo);
                                weaponManager.PlayerInventorySO.upgrades.upgradeStorage.Add(weaponManager.PlayerInventorySO.upgrades.UpgradeTwo);
                            }
                            weaponManager.PlayerInventorySO.upgrades.UpgradeTwo = upgradeSO;
                            break;
                        }
                    case InventoryUpgradeButton.UpgradeSlotType.Three: {
                            if (weaponManager.PlayerInventorySO.upgrades.UpgradeThree) {
                                GameManager.Instance.UIManager.RemoveUpgrade(ref weaponManager.PlayerInventorySO.upgrades.UpgradeThree);
                                weaponManager.PlayerInventorySO.upgrades.upgradeStorage.Add(weaponManager.PlayerInventorySO.upgrades.UpgradeThree);
                            }
                            weaponManager.PlayerInventorySO.upgrades.UpgradeThree = upgradeSO;
                            break;
                        }
                }

                GameManager.Instance.UIManager.AddUpgrade(ref upgradeSO);
                weaponManager.PlayerInventorySO.upgrades.upgradeStorage.Remove(upgradeSO);

                GameManager.Instance.UIManager.SetupInventoryUI(GameManager.Instance.PlayerManager.GetPlayer.GetComponent<WeaponManager>().PlayerInventorySO);
                GameManager.Instance.UIManager.SetupInventoryUpgradeList();
            }
        });
    }
}
