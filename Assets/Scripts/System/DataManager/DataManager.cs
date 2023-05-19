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
    public string Username { get => username; set => username = value; }
    public bool IsRetry { get => isRetry; set => isRetry = value; }
    #endregion

    #region private var
    [SerializeField] private string username;
    [SerializeField] private bool isRetry;
    #endregion

    private void Awake()
    {
        HandleSingletonObject();
        LoadComponents();
        DontDestroyOnLoad(this);
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

    private void HandleSingletonObject()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
