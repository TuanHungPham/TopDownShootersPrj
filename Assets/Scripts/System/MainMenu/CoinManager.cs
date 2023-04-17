using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    #region public var
    public int coin;
    #endregion

    #region private var
    #endregion

    private void OnEnable()
    {
        UpdateCoin();
    }

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
    }

    private void UpdateCoin()
    {
        coin += DataManager.Instance.achievementDataManager.coin;
    }

    public void ConsumeCoin(int consumptionQuantity)
    {
        coin -= consumptionQuantity;
    }
}
