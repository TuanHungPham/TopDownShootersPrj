using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private void Update()
    {
        ShowCoin();
    }

    private void ShowCoin()
    {
        coinText.text = UserManager.Instance.mainAchievementData.coin.ToString();
    }
}
