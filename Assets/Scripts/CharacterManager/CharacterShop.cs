using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShop : MonoBehaviour
{
    #region public var
    public bool IsCharacterCanBeOwned { get => isCharacterCanBeOwned; set => isCharacterCanBeOwned = value; }
    #endregion

    #region private var
    [SerializeField] private CharacterManagerCtrl characterManagerCtrl;
    [SerializeField] private bool isCharacterCanBeOwned;
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
        characterManagerCtrl = GetComponent<CharacterManagerCtrl>();
    }

    private void Update()
    {
        CheckCanBuy();
    }

    private void CheckCanBuy()
    {
        if (characterManagerCtrl.selectedCharacter == null) return;

        CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.selectedCharacter.GetComponent<CharacterDisplayCtrl>();

        if (!characterDisplayCtrl.characterData.IsOwned)
        {
            isCharacterCanBeOwned = true;
            return;
        }
        isCharacterCanBeOwned = false;
    }

    public void Buy()
    {
        CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.selectedCharacter.GetComponent<CharacterDisplayCtrl>();

        if (UserManager.Instance.mainAchievementData.coin < characterDisplayCtrl.characterData.coinRequirement) return;

        UserManager.Instance.mainAchievementData.ConsumeCoin(characterDisplayCtrl.characterData.coinRequirement);
        characterDisplayCtrl.characterData.IsOwned = true;
        characterDisplayCtrl.characterData.characterLevel = 1;
    }
}
