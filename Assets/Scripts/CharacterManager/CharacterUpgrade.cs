using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpgrade : MonoBehaviour
{
    #region public var
    public int hpUpgrade;
    #endregion

    #region private var
    [SerializeField] private UserManager userManager;
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
        userManager = GameObject.Find("UserManager").GetComponent<UserManager>();
        characterManagerCtrl = GetComponent<CharacterManagerCtrl>();
    }

    public void Upgrade()
    {
        CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.selectedCharacter.GetComponent<CharacterDisplayCtrl>();

        // if (UserManager.Instance.mainAchievementData.coin < characterDisplayCtrl.characterData.coinRequirement) return;
        if (userManager.mainAchievementData.coin < characterDisplayCtrl.characterData.coinRequirement) return;

        // UserManager.Instance.mainAchievementData.ConsumeCoin(characterDisplayCtrl.characterData.coinRequirement);
        userManager.mainAchievementData.ConsumeCoin(characterDisplayCtrl.characterData.coinRequirement);

        characterDisplayCtrl.characterData.characterLevel++;
        characterDisplayCtrl.characterData.characterHP += hpUpgrade;
        characterDisplayCtrl.characterData.upgradePrice += characterDisplayCtrl.characterData.upgradePriceAdd;
    }
}
