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
        CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.SelectedCharacter.GetComponent<CharacterDisplayCtrl>();

        if (UserManager.Instance.Coin < characterDisplayCtrl.CharacterData.coinRequirement) return;

        UserManager.Instance.ConsumeCoin(characterDisplayCtrl.CharacterData.coinRequirement);
        characterDisplayCtrl.CharacterData.characterLevel++;
        characterDisplayCtrl.CharacterData.characterHP += hpUpgrade;
        characterDisplayCtrl.CharacterData.upgradePrice += characterDisplayCtrl.CharacterData.upgradePriceAdd;
    }
}
