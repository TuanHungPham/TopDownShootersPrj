using UnityEngine;
using TigerForge;
using System;

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
    private LoadedCharacterData loadedCharacterData;
    private IKeyValueDatabase databaseInstance;
    #endregion

    private void Awake()
    {
        HandleSingletonObject();
        DontDestroyOnLoad(this);
        LoadComponents();
    }

    private void Start()
    {
        LoadInGameData();
        ListenEvent();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        CharacterDataManager = GetComponentInChildren<CharacterDataManager>();
        AchievementDataManager = GetComponentInChildren<AchievementDataManager>();

        databaseInstance = new PlayfabDatabase();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.PLAY_GAME.ToString(), SaveInGameCharacterShop);
        EventManager.StartListening(EventID.PLAY_GAME.ToString(), SaveInGameData);
        EventManager.StartListening(EventID.PLAYFAB_LOGGINGIN.ToString(), LoadDataFromDatabase);
        EventManager.StartListening(EventID.PLAYFAB_LOADING_DATA.ToString(), LoadInGameData);
        EventManager.StartListening(EventID.PLAYFAB_LOADING_DATA.ToString(), LoadInGameCharacterShop);
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

    public void SaveInGameData()
    {
        databaseInstance.SetInGameData(DatabaseKey.HIGHEST_ENEMIES_KILLED.ToString(), achievementDataManager.HighestEnemiesKilled);
        databaseInstance.SetInGameData(DatabaseKey.COIN.ToString(), achievementDataManager.Coin);
        databaseInstance.SetInGameData(DatabaseKey.HIGHEST_SURVIVAL_TIME.ToString(), achievementDataManager.HighestSurvivalTime);
    }

    public void LoadInGameData()
    {
        achievementDataManager.Coin = databaseInstance.GetInGameData<int>(DatabaseKey.COIN.ToString());
        achievementDataManager.HighestEnemiesKilled = databaseInstance.GetInGameData<int>(DatabaseKey.HIGHEST_ENEMIES_KILLED.ToString());
        achievementDataManager.HighestSurvivalTime = databaseInstance.GetInGameData<float>(DatabaseKey.HIGHEST_SURVIVAL_TIME.ToString());

        EventManager.EmitEvent(EventID.CHANGING_COIN_QUANTITY.ToString());
    }

    public void SaveInGameCharacterShop()
    {
        foreach (Transform character in CharacterManagerCtrl.Instance.listOfCharacter)
        {
            CharacterDisplayCtrl characterDisplayCtrl = character.GetComponent<CharacterDisplayCtrl>();
            CharacterData characterData = characterDisplayCtrl.CharacterData;

            loadedCharacterData = new LoadedCharacterData
            {
                characterSkinIndex = characterData.characterSkinIndex,
                characterName = characterData.characterName,
                characterLevel = characterData.characterLevel,
                characterHP = characterData.characterHP,
                upgradePrice = characterData.upgradePrice,
                buyPrice = characterData.BuyPrice,
                isOwned = characterData.IsOwned
            };

            string key = character.name;

            databaseInstance.SetInGameData(key, loadedCharacterData);
        }
    }

    public void LoadInGameCharacterShop()
    {
        foreach (Transform character in CharacterManagerCtrl.Instance.listOfCharacter)
        {
            CharacterDisplayCtrl characterDisplayCtrl = character.GetComponent<CharacterDisplayCtrl>();
            CharacterData characterData = characterDisplayCtrl.CharacterData;

            string key = character.name;

            loadedCharacterData = databaseInstance.GetInGameData<LoadedCharacterData>(key);

            if (loadedCharacterData == null) return;

            characterData.SetData(loadedCharacterData);
        }
    }

    public void LoadDataFromDatabase()
    {
        databaseInstance.LoadFromDatabase();
    }

    private void OnApplicationQuit()
    {
        SaveInGameCharacterShop();
        SaveInGameData();
        databaseInstance.SaveToDatabase();
    }
}