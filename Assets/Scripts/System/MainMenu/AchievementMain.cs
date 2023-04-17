using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementMain : MonoBehaviour
{
    private static AchievementMain instance;

    public static AchievementMain Instance { get => instance; set => instance = value; }
    #region public var
    public CoinManager coinManager;
    #endregion

    #region private var
    #endregion

    private void Awake()
    {
        instance = this;
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        coinManager = GetComponentInChildren<CoinManager>();
    }
}
