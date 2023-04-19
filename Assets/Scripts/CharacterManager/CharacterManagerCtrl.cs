using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerCtrl : MonoBehaviour
{
    private static CharacterManagerCtrl instance;
    public static CharacterManagerCtrl Instance { get => instance; }

    public List<Transform> listOfCharacter = new List<Transform>();
    public Transform selectedCharacter;

    public CharacterShop characterShop;
    public CharacterUpgrade characterUpgrade;

    public DisplayPointManager displayPointManager;


    private void Awake()
    {
        instance = this;
        LoadComponents();
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

            if (DataManager.Instance == null) return;
            DataManager.Instance.characterDataManager.GetCharacterData();
            return;
        }
    }
}
