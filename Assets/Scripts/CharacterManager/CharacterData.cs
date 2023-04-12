using System.Collections;
using System.Collections.Generic;
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
        upgradePrice = 5000;
        upgradePriceAdd = 500;
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

        coinRequirement = buyPrice;
    }
}
