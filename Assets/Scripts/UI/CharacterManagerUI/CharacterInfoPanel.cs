using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInfoPanel : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private CharacterManagerCtrl characterManagerCtrl;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text hpAddMoreText;
    [SerializeField] private Image primaryWeaponImage;
    [SerializeField] private Image secondaryWeaponImage;
    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        characterManagerCtrl = GameObject.Find("------ MANAGER ------").GetComponentInChildren<CharacterManagerCtrl>();

        hpAddMoreText = transform.Find("HPAddMore").GetComponent<TMP_Text>();
        hpText = transform.GetChild(0).Find("HPStatus").Find("HPText").GetComponent<TMP_Text>();
        primaryWeaponImage = transform.GetChild(0).Find("PrimaryWeapon").GetComponent<Image>();
        secondaryWeaponImage = transform.GetChild(0).Find("SecondaryWeapon").GetComponent<Image>();
    }

    private void Update()
    {
        ShowCharacterInfo();
    }

    private void ShowCharacterInfo()
    {
        if (characterManagerCtrl.SelectedCharacter == null) return;

        CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.SelectedCharacter.GetComponent<CharacterDisplayCtrl>();

        ShowHP(characterDisplayCtrl.CharacterData.characterHP);
        ShowWeapon(characterDisplayCtrl.CharacterData.primaryWeaponData, characterDisplayCtrl.CharacterData.secondaryWeaponData);
        ShowUpgradeHPQuantity();
    }

    private void ShowHP(int hp)
    {
        hpText.text = hp.ToString();
    }

    private void ShowUpgradeHPQuantity()
    {
        hpAddMoreText.text = "+ " + characterManagerCtrl.CharacterUpgrade.hpUpgrade.ToString();
    }

    private void ShowWeapon(WeaponData primaryWeapon, WeaponData secondaryWeapon)
    {
        primaryWeaponImage.sprite = primaryWeapon.WeaponSprite;
        secondaryWeaponImage.sprite = secondaryWeapon.WeaponSprite;
    }
}
