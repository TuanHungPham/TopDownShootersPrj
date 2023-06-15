using TigerForge;
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

        if (DataManager.Instance.AchievementDataManager.Coin < characterDisplayCtrl.CharacterData.coinRequirement) return;

        DataManager.Instance.AchievementDataManager.ConsumeCoin(characterDisplayCtrl.CharacterData.coinRequirement);

        characterDisplayCtrl.CharacterData.characterLevel++;
        characterDisplayCtrl.CharacterData.characterHP += hpUpgrade;
        characterDisplayCtrl.CharacterData.upgradePrice += characterDisplayCtrl.CharacterData.upgradePriceAdd;

        EventManager.EmitEvent(EventID.CHANGING_COIN_QUANTITY.ToString());
    }
}
