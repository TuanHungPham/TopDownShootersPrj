using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterManagerUI : MonoBehaviour
{
    #region public var
    public ButtonManager buttonManager;
    public CharacterInfoPanel characterInfoPanel;
    #endregion

    #region private var
    [SerializeField] private CharacterManagerCtrl characterManagerCtrl;
    [SerializeField] private TMP_Text usernameText;
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
        usernameText = transform.Find("UsernameUI").GetComponent<TMP_Text>();
        buttonManager = GetComponentInChildren<ButtonManager>();
        characterInfoPanel = GetComponentInChildren<CharacterInfoPanel>();

        characterManagerCtrl = GameObject.Find("------ MANAGER ------").GetComponentInChildren<CharacterManagerCtrl>();
    }

    public void ShowUsername()
    {
        usernameText.text = UserManager.Instance.userName;
    }
}
