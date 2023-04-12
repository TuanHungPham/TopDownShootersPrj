using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerUI : MonoBehaviour
{
    #region public var
    public ButtonManager buttonManager;
    public CoinPanel coinPanel;
    public CharacterInfoPanel characterInfoPanel;
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
        buttonManager = GetComponentInChildren<ButtonManager>();
        coinPanel = GetComponentInChildren<CoinPanel>();
        characterInfoPanel = GetComponentInChildren<CharacterInfoPanel>();

        characterManagerCtrl = GameObject.Find("------ MANAGER ------").GetComponentInChildren<CharacterManagerCtrl>();
    }
}
