using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataManager : MonoBehaviour
{
    #region public var
    public int characterHP;
    public int characterSkinIndex;


    [Space(20)]
    public WeaponData primaryWeaponData;
    public WeaponData secondaryWeaponData;
    #endregion

    #region private var
    [Space(20)]
    [SerializeField] private CharacterData selectedCharacterData;
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
    }

    public void GetCharacterData()
    {
        if (CharacterManagerCtrl.Instance.selectedCharacter == null) return;

        CharacterDisplayCtrl characterDisplayCtrl = CharacterManagerCtrl.Instance.selectedCharacter.GetComponent<CharacterDisplayCtrl>();
        selectedCharacterData = characterDisplayCtrl.characterData;

        if (!selectedCharacterData.IsOwned) return;

        characterHP = selectedCharacterData.characterHP;
        characterSkinIndex = selectedCharacterData.characterSkinIndex;
        primaryWeaponData = selectedCharacterData.primaryWeaponData;
        secondaryWeaponData = selectedCharacterData.secondaryWeaponData;
    }
}
