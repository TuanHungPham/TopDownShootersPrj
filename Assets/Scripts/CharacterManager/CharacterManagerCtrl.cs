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

    private void Start()
    {
        LoadDataFromMainData();
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

    private void LoadDataFromMainData()
    {
        DataManager.Instance.LoadCharacterShop();
    }
}
