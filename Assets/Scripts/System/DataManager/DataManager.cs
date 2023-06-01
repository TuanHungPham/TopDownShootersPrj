using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance { get => instance; }

    #region public var
    public string Username { get => username; set => username = value; }
    public bool IsRetry { get => isRetry; set => isRetry = value; }
    public CharacterDataManager CharacterDataManager { get => characterDataManager; set => characterDataManager = value; }
    public AchievementDataManager AchievementDataManager { get => achievementDataManager; set => achievementDataManager = value; }
    #endregion

    #region private var
    [SerializeField] private string username;
    [SerializeField] private bool isRetry;
    [SerializeField] private CharacterDataManager characterDataManager;
    [SerializeField] private AchievementDataManager achievementDataManager;
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
        CharacterDataManager = GetComponentInChildren<CharacterDataManager>();
        AchievementDataManager = GetComponentInChildren<AchievementDataManager>();
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
