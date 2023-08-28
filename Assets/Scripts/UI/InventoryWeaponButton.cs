using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeaponButton : MonoBehaviour {

    public enum WeaponSlotType {
        One, Two, None
    }

    [SerializeField] private WeaponSlotType weaponSlotType;
    [SerializeField] private Button baseButton;
    [SerializeField] private Button removeButton;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image selectImage;
    [SerializeField] private TextMeshProUGUI upgradeText;


    public void SetupButton(EquippedSO equippedSO) {

        baseButton.onClick.RemoveAllListeners();
        //removeButton.onClick.RemoveAllListeners();

        Debug.Log("Adding listener");
        baseButton.onClick.AddListener(() => {
            Debug.Log("CLicking weapon");
            GameManager.Instance.UIManager.SelectWeaponSlot(weaponSlotType);
        });

        if (equippedSO == null) {
            ClearSlotVisual();
            return;
        }

        SetSlotVisual(equippedSO);

        WeaponManager weaponManager = GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>();

/*        removeButton.onClick.AddListener(() => {
            //weaponManager.UnequipSlot(upgradeSlotType);
        });*/
    }

    private void ClearSlotVisual() {
        iconImage.sprite = null;
        iconImage.color = Color.white;
        upgradeText.text = null;
        //removeButton.gameObject.SetActive(false);
    }

    private void SetSlotVisual(EquippedSO equippedSO) {
        iconImage.sprite = equippedSO.weaponSO.weaponShopData.icon;
        //iconImage.color = upgradeSO.IconTint;
        upgradeText.text = equippedSO.weaponSO.weaponData.weaponName;
        //removeButton.gameObject.SetActive(true);
    }

    public void Select() {
        selectImage.gameObject.SetActive(true);
    }

    public void Deselect() {
        selectImage.gameObject.SetActive(false);
    }
}
