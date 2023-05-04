using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpgrade : MonoBehaviour
{
    #region public var
    public int hpUpgrade;
    #endregion

    #region private var
    [SerializeField] private CharacterManagerCtrl characterManagerCtrl;
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

    public void Upgrade()
    {
        CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.selectedCharacter.GetComponent<CharacterDisplayCtrl>();

        if (UserManager.Instance.coin < characterDisplayCtrl.characterData.coinRequirement) return;

        UserManager.Instance.ConsumeCoin(characterDisplayCtrl.characterData.coinRequirement);
        characterDisplayCtrl.characterData.characterLevel++;
        characterDisplayCtrl.characterData.characterHP += hpUpgrade;
        characterDisplayCtrl.characterData.upgradePrice += characterDisplayCtrl.characterData.upgradePriceAdd;
    }
}
