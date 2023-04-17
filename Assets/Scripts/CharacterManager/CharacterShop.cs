using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShop : MonoBehaviour
{
    #region public var
    public bool IsCharacterOwned { get => isCharacterOwned; set => isCharacterOwned = value; }
    #endregion

    #region private var
    [SerializeField] private UserManager userManager;
    [SerializeField] private CharacterManagerCtrl characterManagerCtrl;
    [SerializeField] private bool isCharacterOwned;
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
            isCharacterOwned = true;
            return;
        }
        isCharacterOwned = false;
    }

    public void Buy()
    {
        CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.selectedCharacter.GetComponent<CharacterDisplayCtrl>();

        // if (UserManager.Instance.mainAchievementData.coin < characterDisplayCtrl.characterData.coinRequirement) return;
        if (userManager.mainAchievementData.coin < characterDisplayCtrl.characterData.coinRequirement) return;

        // UserManager.Instance.mainAchievementData.ConsumeCoin(characterDisplayCtrl.characterData.coinRequirement);
        userManager.mainAchievementData.ConsumeCoin(characterDisplayCtrl.characterData.coinRequirement);

        characterDisplayCtrl.characterData.IsOwned = true;
        characterDisplayCtrl.characterData.characterLevel = 1;
    }
}
