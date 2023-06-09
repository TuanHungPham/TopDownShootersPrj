using UnityEngine;

public class CharacterData : MonoBehaviour
{
    #region public var
    public int characterSkinIndex;

    [Space(20)]
    public string characterName;
    public int characterLevel;
    public int characterHP;
    public WeaponData primaryWeaponData;
    public WeaponData secondaryWeaponData;

    [Space(20)]
    public int coinRequirement;
    public int upgradePrice;
    public int upgradePriceAdd;
    public int BuyPrice { get => buyPrice; set => buyPrice = value; }
    public bool IsOwned { get => isOwned; set => isOwned = value; }
    #endregion

    #region private var
    [SerializeField] private int buyPrice;
    [SerializeField] private bool isOwned;
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
        characterName = transform.parent.name;
    }

    private void Update()
    {
        CheckPrice();
    }

    private void CheckPrice()
    {
        if (isOwned)
        {
            coinRequirement = upgradePrice;
            return;
        }

        coinRequirement = BuyPrice;
    }

    public void SetData(LoadedCharacterData loadedCharacterData)
    {
        characterSkinIndex = loadedCharacterData.characterSkinIndex;
        characterName = loadedCharacterData.characterName;
        characterLevel = loadedCharacterData.characterLevel;
        characterHP = loadedCharacterData.characterHP;
        BuyPrice = loadedCharacterData.buyPrice;
        upgradePrice = loadedCharacterData.upgradePrice;
        isOwned = loadedCharacterData.isOwned;
    }
}
