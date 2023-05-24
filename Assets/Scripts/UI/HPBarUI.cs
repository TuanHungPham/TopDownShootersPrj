using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    #region public var
    public Slider hpSlider;
    public Slider hpBGSlider;
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private TMP_Text hpText;
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
        hpBGSlider = transform.Find("HPBarBG").Find("HPBGSlider").GetComponent<Slider>();

        playerCtrl = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").GetComponent<PlayerCtrl>();

        smoothTime = 0.5f;
    }

    public void InitializeHPBar()
    {
        hpSlider.maxValue = playerCtrl.playerStatus.maxHP;
        hpSlider.value = playerCtrl.playerStatus.currentHP;
        hpBGSlider.maxValue = hpSlider.maxValue;
        hpBGSlider.value = hpSlider.value;
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

        if (hpSlider.value < playerCtrl.playerStatus.currentHP)
        {
            hpSlider.value += smoothTime;
            Invoke("SetHPBGSlider", 0.4f);
        }
        else
        {
            hpSlider.value -= smoothTime;
            Invoke("SetHPBGSlider", 0.4f);
        }
    }

    private void SetHPBGSlider()
    {
        if (hpBGSlider.value == hpSlider.value) return;

        if (hpBGSlider.value < hpSlider.value)
        {
            hpBGSlider.value += smoothTime;
        }
        else
        {
            hpBGSlider.value -= smoothTime;
        }
    }
}
