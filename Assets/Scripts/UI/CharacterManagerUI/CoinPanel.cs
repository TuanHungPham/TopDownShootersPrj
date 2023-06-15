using TMPro;
using UnityEngine;
using TigerForge;
using System;

public class CoinPanel : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private TMP_Text coinText;
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
        coinText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        ListenEvent();
        ShowCoin();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CHANGING_COIN_QUANTITY.ToString(), ShowCoin);
    }

    private void ShowCoin()
    {
        coinText.text = DataManager.Instance.AchievementDataManager.Coin.ToString();
    }
}
