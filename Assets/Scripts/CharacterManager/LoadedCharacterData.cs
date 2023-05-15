[System.Serializable]
public class LoadedCharacterData
{
    #region public var
    public int CharacterSkinIndex { get => characterSkinIndex; set => characterSkinIndex = value; }
    public string CharacterName { get => characterName; set => characterName = value; }
    public int CharacterLevel { get => characterLevel; set => characterLevel = value; }
    public int CharacterHP { get => characterHP; set => characterHP = value; }
    public int BuyPrice { get => buyPrice; set => buyPrice = value; }
    public bool IsOwned { get => isOwned; set => isOwned = value; }
    #endregion

    #region private var
    private int characterSkinIndex;
    private string characterName;
    private int characterLevel;
    private int characterHP;
    private int coinRequirement;
    private int upgradePrice;
    private int upgradePriceAdd;
    private int buyPrice;
    private bool isOwned;
    #endregion
}
