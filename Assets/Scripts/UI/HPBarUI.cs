using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPBarUI : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private PlayerCtrl playerCtrl;
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
        hpText = GetComponentInChildren<TMP_Text>();
        playerCtrl = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").GetComponent<PlayerCtrl>();
    }

    private void Update()
    {
        ShowHP();
    }

    private void ShowHP()
    {
        hpText.text = string.Format("HP: {0}/{1}", playerCtrl.playerStatus.currentHP, playerCtrl.playerStatus.maxHP);
    }
}
