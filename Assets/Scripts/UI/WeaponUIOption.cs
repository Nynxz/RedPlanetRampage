using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIOption : MonoBehaviour {
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI upgradeName;

    private int indexInList;

    public void Setup(EquippedSO equippedSO, int indexInList) {
        this.indexInList = indexInList;
        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        iconImage.sprite = equippedSO.weaponSO.weaponShopData.icon;
        //iconImage.color = equippedSO.weaponSO.weaponShopData.iconTint;
        upgradeName.text = equippedSO.weaponSO.weaponData.weaponName;

        gameObject.GetComponent<Button>().onClick.AddListener(() => {

            if (GameManager.Instance.UIManager.currentSelectedWeaponSlot != InventoryWeaponButton.WeaponSlotType.None) { // If a slot is selected

                WeaponManager weaponManager = GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>();

                weaponManager.SwapSlotWithWeaponIndex(GameManager.Instance.UIManager.currentSelectedWeaponSlot, this.indexInList);
            }
        });
    }
}
