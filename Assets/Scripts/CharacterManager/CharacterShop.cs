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
        if (characterManagerCtrl.SelectedCharacter == null) return;

        CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.SelectedCharacter.GetComponent<CharacterDisplayCtrl>();

        if (!characterDisplayCtrl.CharacterData.IsOwned)
        {
            isCharacterCanBeOwned = true;
            return;
        }
        isCharacterCanBeOwned = false;
    }

    public void Buy()
    {
        CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.SelectedCharacter.GetComponent<CharacterDisplayCtrl>();

        if (UserManager.Instance.Coin < characterDisplayCtrl.CharacterData.coinRequirement) return;

        UserManager.Instance.ConsumeCoin(characterDisplayCtrl.CharacterData.coinRequirement);
        characterDisplayCtrl.CharacterData.IsOwned = true;
        characterDisplayCtrl.CharacterData.characterLevel = 1;
    }
}
