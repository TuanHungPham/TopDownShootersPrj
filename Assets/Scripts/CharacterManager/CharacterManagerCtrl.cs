using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerCtrl : MonoBehaviour
{
    private static CharacterManagerCtrl instance;
    public static CharacterManagerCtrl Instance { get => instance; }

    #region public var
    public List<Transform> listOfCharacter = new List<Transform>();
    public Transform SelectedCharacter { get => selectedCharacter; set => selectedCharacter = value; }
    public CharacterUpgrade CharacterUpgrade { get => characterUpgrade; set => characterUpgrade = value; }
    public CharacterShop CharacterShop { get => characterShop; set => characterShop = value; }
    #endregion

    #region private var
    [SerializeField] private Transform selectedCharacter;
    [SerializeField] private CharacterShop characterShop;
    [SerializeField] private CharacterUpgrade characterUpgrade;
    [SerializeField] private DisplayPointManager displayPointManager;
    private LoadedCharacterData loadedCharacterData;
    #endregion

    private void Awake()
    {
        instance = this;
        LoadComponents();
        Load();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Start()
    {
        SetDefaultCharacter();
        GetSelectedCharacter();
    }

    private void LoadComponents()
    {
        CharacterShop = GetComponent<CharacterShop>();
        CharacterUpgrade = GetComponent<CharacterUpgrade>();
        displayPointManager = transform.parent.GetComponentInChildren<DisplayPointManager>();

        InitializeCharacterList();
    }

    private void Update()
    {
        GetSelectedCharacter();
    }

    private void InitializeCharacterList()
    {
        if (listOfCharacter.Count > 4) return;

        foreach (Transform character in transform)
        {
            if (listOfCharacter.Contains(character)) continue;

            listOfCharacter.Add(character);
        }
    }

    private void SetDefaultCharacter()
    {
        Transform character = listOfCharacter.Find((x) => x.name.Equals("Character1"));

        CharacterDisplayCtrl characterDisplayCtrl = character.GetComponent<CharacterDisplayCtrl>();
        characterDisplayCtrl.CharacterData.IsOwned = true;
    }

    private void GetSelectedCharacter()
    {
        foreach (Transform character in listOfCharacter)
        {
            CharacterDisplayCtrl characterDisplayCtrl = character.GetComponent<CharacterDisplayCtrl>();
            if (!characterDisplayCtrl.IsSelected) continue;

            SelectedCharacter = character;

            DataManager.Instance.CharacterDataManager.GetSelectedCharacterData();
            return;
        }
    }

    private void Save()
    {
        foreach (Transform character in listOfCharacter)
        {
            CharacterDisplayCtrl characterDisplayCtrl = character.GetComponent<CharacterDisplayCtrl>();
            CharacterData characterData = characterDisplayCtrl.CharacterData;

            loadedCharacterData = new LoadedCharacterData
            (
                characterData.characterSkinIndex,
                characterData.characterName,
                characterData.characterLevel,
                characterData.characterHP,
                characterData.upgradePrice,
                characterData.BuyPrice,
                characterData.IsOwned
            );

            string fileName = character.name;
            string json = JsonUtility.ToJson(loadedCharacterData);

            IOSystem.WriteToFile(fileName, json);
        }

        Debug.Log("Character data is saved!");
    }

    private void Load()
    {
        foreach (Transform character in listOfCharacter)
        {
            CharacterDisplayCtrl characterDisplayCtrl = character.GetComponent<CharacterDisplayCtrl>();
            CharacterData characterData = characterDisplayCtrl.CharacterData;

            string fileName = character.name;

            string json = IOSystem.ReadFromFIle(fileName);
            if (json == null) continue;

            loadedCharacterData = JsonUtility.FromJson<LoadedCharacterData>(json);

            characterData.SetData(loadedCharacterData);
            Debug.Log(character.name + " data is loaded!");
        }
    }

    private void OnDisable()
    {
        Save();
    }
}
