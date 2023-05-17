public class LoadedCharacterData
{
    #region public var
    #endregion

    #region private var
    public int characterSkinIndex;
    public string characterName;
    public int characterLevel;
    public int characterHP;
    public int coinRequirement;
    public int upgradePrice;
    public int upgradePriceAdd;
    public int buyPrice;
    public bool isOwned;
    #endregion

    public LoadedCharacterData(int skinIndex, string name, int level, int hp, int upgradePrice, int buyPrice, bool isOwned)
    {
        characterSkinIndex = skinIndex;
        characterName = name;
        characterLevel = level;
        characterHP = hp;
        this.upgradePrice = upgradePrice;
        this.buyPrice = buyPrice;
        this.isOwned = isOwned;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
