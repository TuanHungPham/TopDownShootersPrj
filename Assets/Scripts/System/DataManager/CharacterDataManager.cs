using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterDataManager : MonoBehaviour
{
    #region public var
    public int characterHP;
    public int characterSkinIndex;


    [Space(20)]
    public WeaponData primaryWeaponData;
    public WeaponData secondaryWeaponData;
    public bool IsDataLoaded { get => isDataLoaded; set => isDataLoaded = value; }
    #endregion

    #region private var
    [Space(20)]
    [SerializeField] private CharacterData selectedCharacterData;

    [Space(20)]
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private bool isDataLoaded;

    [Space(20)]
    [SerializeField] private Scene currentScene;
    #endregion

    private void Update()
    {
        GetCurrentScene();
        SetIngameCharacterData();
    }

    private void GetCurrentScene()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void GetSelectedCharacterData()
    {
        if (CharacterManagerCtrl.Instance.selectedCharacter == null) return;

        CharacterDisplayCtrl characterDisplayCtrl = CharacterManagerCtrl.Instance.selectedCharacter.GetComponent<CharacterDisplayCtrl>();
        selectedCharacterData = characterDisplayCtrl.characterData;

        if (!selectedCharacterData.IsOwned) return;

        characterHP = selectedCharacterData.characterHP;
        characterSkinIndex = selectedCharacterData.characterSkinIndex;
        primaryWeaponData = selectedCharacterData.primaryWeaponData;
        secondaryWeaponData = selectedCharacterData.secondaryWeaponData;
    }

    public void SetIngameCharacterData()
    {
        if (!currentScene.name.Equals("InGameScene"))
        {
            IsDataLoaded = false;
            return;
        }

        playerCtrl = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").GetComponent<PlayerCtrl>();

        if (IsDataLoaded) return;

        SetHP();
        SetCharacterSkin();
        SetInitialWeapons();
        SetInitialAmmo();

        Debug.Log("Data is loaded!");
        IsDataLoaded = true;
    }

    private void SetHP()
    {
        playerCtrl.playerStatus.maxHP = DataManager.Instance.characterDataManager.characterHP;
        playerCtrl.playerStatus.currentHP = playerCtrl.playerStatus.maxHP;

        UIManager.Instance.hPBarUI.InitializeHPBar();
    }

    private void SetCharacterSkin()
    {
        CharacterSkinManager.Instance.SetSkin(DataManager.Instance.characterDataManager.characterSkinIndex);
    }

    private void SetInitialWeapons()
    {
        playerCtrl.playerWeaponInventory.weaponInventory.Add(DataManager.Instance.characterDataManager.primaryWeaponData);
        playerCtrl.playerWeaponInventory.weaponInventory.Add(DataManager.Instance.characterDataManager.secondaryWeaponData);
        playerCtrl.playerWeaponInventory.IsUpdateInventory = true;

        playerCtrl.playerSwapWeaponSystem.GetWeaponFromStorage();
        playerCtrl.playerWeaponSystem.GetWeaponInHolder();
    }

    private void SetInitialAmmo()
    {
        playerCtrl.ammoSystem.rifleAmmo = DataManager.Instance.characterDataManager.primaryWeaponData.InitialAmmo;
        playerCtrl.ammoSystem.pistolAmmo = DataManager.Instance.characterDataManager.secondaryWeaponData.InitialAmmo;
    }
}
