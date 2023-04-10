using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private float smoothTime;
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
        hpText = transform.Find("TextBG").GetComponentInChildren<TMP_Text>();
        hpSlider = transform.Find("HPBarBG").Find("HPSlider").GetComponent<Slider>();

        playerCtrl = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").GetComponent<PlayerCtrl>();

        smoothTime = 0.5f;
    }

    private void Update()
    {
        ShowHP();
    }

    private void ShowHP()
    {
        hpText.text = string.Format("{0}/{1}", playerCtrl.playerStatus.currentHP, playerCtrl.playerStatus.maxHP);

        SetHPSlider();
    }

    private void SetHPSlider()
    {
        if (hpSlider.value == playerCtrl.playerStatus.currentHP) return;

        hpSlider.value -= smoothTime;
    }
}
