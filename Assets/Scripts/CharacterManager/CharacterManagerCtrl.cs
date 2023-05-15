using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterManagerCtrl : MonoBehaviour
{
    private static CharacterManagerCtrl instance;
    public static CharacterManagerCtrl Instance { get => instance; }

    #region public var
    public List<Transform> listOfCharacter = new List<Transform>();
    public Transform selectedCharacter;
    public CharacterShop characterShop;
    public CharacterUpgrade characterUpgrade;
    public DisplayPointManager displayPointManager;
    #endregion

    #region private var
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
        characterShop = GetComponent<CharacterShop>();
        characterUpgrade = GetComponent<CharacterUpgrade>();
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
        characterDisplayCtrl.characterData.characterLevel = 1;
        characterDisplayCtrl.characterData.IsOwned = true;
    }

    private void GetSelectedCharacter()
    {
        foreach (Transform character in listOfCharacter)
        {
            CharacterDisplayCtrl characterDisplayCtrl = character.GetComponent<CharacterDisplayCtrl>();
            if (!characterDisplayCtrl.IsSelected) continue;

            selectedCharacter = character;

            DataManager.Instance.characterDataManager.GetSelectedCharacterData();
            return;
        }
    }

    private void Save()
    {
        foreach (Transform character in listOfCharacter)
        {
            CharacterDisplayCtrl characterDisplayCtrl = character.GetComponent<CharacterDisplayCtrl>();
            CharacterData characterData = characterDisplayCtrl.characterData;

            string fileName = character.name;
            string json = JsonUtility.ToJson(characterData);

            IOSystem.WriteToFile(fileName, json);
        }

        Debug.Log("Character data is saved!");
    }

    private void Load()
    {
        foreach (Transform character in listOfCharacter)
        {
            CharacterDisplayCtrl characterDisplayCtrl = character.GetComponent<CharacterDisplayCtrl>();
            CharacterData characterData = characterDisplayCtrl.characterData;
            LoadedCharacterData loadedCharacterData = new LoadedCharacterData();

            string fileName = character.name;

            string json = IOSystem.ReadFromFIle(fileName);
            if (json == null) continue;

            JsonUtility.FromJsonOverwrite(json, loadedCharacterData);

            characterData.SetData(loadedCharacterData);
            Debug.Log(character.name + " buy price: " + loadedCharacterData.BuyPrice);
            Debug.Log(character.name + " data is loaded!");
        }
    }

    private void OnDisable()
    {
        Save();
    }
}
