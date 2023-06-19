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
        LoadData();
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

        databaseInstance = new KeyValueFileDatabase();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.PLAY_GAME.ToString(), SaveCharacterShop);
        EventManager.StartListening(EventID.PLAY_GAME.ToString(), SaveData);
        EventManager.StartListening(EventID.PLAYFAB_LOADING_DATA.ToString(), LoadData);
        EventManager.StartListening(EventID.PLAYFAB_LOADING_DATA.ToString(), LoadCharacterShop);
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

    public void SaveData()
    {
        databaseInstance.Save(DatabaseKey.HIGHEST_ENEMIES_KILLED.ToString(), achievementDataManager.HighestEnemiesKilled);
        databaseInstance.Save(DatabaseKey.COIN.ToString(), achievementDataManager.Coin);
        databaseInstance.Save(DatabaseKey.HIGHEST_SURVIVAL_TIME.ToString(), achievementDataManager.HighestSurvivalTime);
    }

    public void LoadData()
    {
        achievementDataManager.Coin = databaseInstance.Load<int>(DatabaseKey.COIN.ToString());
        achievementDataManager.HighestEnemiesKilled = databaseInstance.Load<int>(DatabaseKey.HIGHEST_ENEMIES_KILLED.ToString());
        achievementDataManager.HighestSurvivalTime = databaseInstance.Load<float>(DatabaseKey.HIGHEST_SURVIVAL_TIME.ToString());

        EventManager.EmitEvent(EventID.CHANGING_COIN_QUANTITY.ToString());
    }

    public void SaveCharacterShop()
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

            databaseInstance.Save(key, loadedCharacterData);
        }
    }

    public void LoadCharacterShop()
    {
        foreach (Transform character in CharacterManagerCtrl.Instance.listOfCharacter)
        {
            CharacterDisplayCtrl characterDisplayCtrl = character.GetComponent<CharacterDisplayCtrl>();
            CharacterData characterData = characterDisplayCtrl.CharacterData;

            string key = character.name;

            loadedCharacterData = databaseInstance.Load<LoadedCharacterData>(key);

            if (loadedCharacterData == null) return;

            characterData.SetData(loadedCharacterData);
        }
    }

    private void OnApplicationQuit()
    {
        SaveCharacterShop();
        SaveData();
    }
}