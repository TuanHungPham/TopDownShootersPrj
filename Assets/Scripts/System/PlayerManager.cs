using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; }

    #region public var
    public CharacterData selectedCharacterData;
    #endregion

    #region private var
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
    }
}
