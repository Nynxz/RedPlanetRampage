using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemVisualWeapon : MonoBehaviour, ISelectHandler {
    private WeaponSO weaponSO;
    public TextMeshProUGUI WeaponName;
    public TextMeshProUGUI WeaponCost;
    public int indexInList;
    public Image bgImage;
    private bool isUnlocked = false;

    [SerializeField] private Image LockImage;
    [SerializeField] private Sprite LockedSprite;
    [SerializeField] private Sprite UnlockedSprite;

    public void OnSelect(BaseEventData eventData) {
        Debug.Log("Selecting");

        GameManager.Instance.NewShopManager.SelectWeapon(indexInList);
    }

    public void Setup(WeaponUnlockS wS, int index) {
        indexInList = index;
        isUnlocked = wS.isUnlocked;
        //weaponSO = wS.weapon;
        WeaponName.text = wS.weapon.weaponData.weaponName;
        WeaponCost.text = wS.weapon.weaponShopData.cost.ToString();
        name = wS.weapon.weaponData.weaponName;

        if(isUnlocked) {
            LockImage.sprite = UnlockedSprite;
            bgImage.color = Color.white;
        } else {
            LockImage.sprite = LockedSprite;
            bgImage.color = Color.gray;
        }
    }
}
