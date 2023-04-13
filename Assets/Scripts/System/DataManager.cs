using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    public static DataManager Instance { get => instance; }

    #region public var
    public int characterHP;
    public int characterSkinIndex;
    public WeaponData primaryWeaponData;
    public WeaponData secondaryWeaponData;
    #endregion

    #region private var
    [SerializeField] private CharacterData selectedCharacterData;
    #endregion

    private void Awake()
    {
        instance = this;
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        DontDestroyOnLoad(this);
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
