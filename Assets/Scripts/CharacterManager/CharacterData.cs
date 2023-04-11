using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    #region public var
    public string characterName;
    public int characterLevel;
    public int buyPrice;
    public int upgradePrice;
    public int upgradePriceAdd;
    public bool CanUpgrade { get => canUpgrade; set => canUpgrade = value; }
    public bool IsOwned { get => isOwned; set => isOwned = value; }
    #endregion

    #region private var
    [SerializeField] private bool isOwned;
    [SerializeField] private bool canUpgrade;
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
        upgradePriceAdd = 5000;
    }
}
