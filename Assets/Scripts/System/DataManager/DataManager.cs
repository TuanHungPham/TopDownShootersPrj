using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    public static DataManager Instance { get => instance; }

    #region public var
    public CharacterDataManager characterDataManager;
    public AchievementDataManager achievementDataManager;
    #endregion

    #region private var
    #endregion

    private void Awake()
    {
        instance = this;
        // DontDestroyOnLoad(this);
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        characterDataManager = GetComponentInChildren<CharacterDataManager>();
        achievementDataManager = GetComponentInChildren<AchievementDataManager>();
    }
}
