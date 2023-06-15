using TigerForge;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterDataManager : MonoBehaviour
{
    #region public var
    public bool IsDataLoaded { get => isDataLoaded; set => isDataLoaded = value; }
    #endregion

    #region private var
    [SerializeField] private int characterHP;
    [SerializeField] private int characterSkinIndex;

    [Space(20)]
    [SerializeField] private WeaponData primaryWeaponData;
    [SerializeField] private WeaponData secondaryWeaponData;
    [SerializeField] private CharacterData selectedCharacterData;
    [SerializeField] private PlayerCtrl playerCtrl;

    [Space(20)]
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
        if (CharacterManagerCtrl.Instance.SelectedCharacter == null) return;

        CharacterDisplayCtrl characterDisplayCtrl = CharacterManagerCtrl.Instance.SelectedCharacter.GetComponent<CharacterDisplayCtrl>();
        selectedCharacterData = characterDisplayCtrl.CharacterData;

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
        playerCtrl.PlayerStatus.MaxHP = DataManager.Instance.CharacterDataManager.characterHP;
        playerCtrl.PlayerStatus.CurrentHP = playerCtrl.PlayerStatus.MaxHP;

        UIManager.Instance.HPBarUI.InitializeHPBar();
    }

    private void SetCharacterSkin()
    {
        CharacterSkinManager.Instance.SetSkin(DataManager.Instance.CharacterDataManager.characterSkinIndex);
    }

    private void SetInitialWeapons()
    {
        playerCtrl.PlayerWeaponInventory.weaponInventory.Add(DataManager.Instance.CharacterDataManager.primaryWeaponData);
        playerCtrl.PlayerWeaponInventory.weaponInventory.Add(DataManager.Instance.CharacterDataManager.secondaryWeaponData);
        EventManager.EmitEvent(EventID.UPDATING_WEAPON_INVENTORY.ToString());

        playerCtrl.PlayerSwapWeaponSystem.GetWeaponFromStorage();
        playerCtrl.PlayerWeaponSystem.GetWeaponInHolder();
    }

    private void SetInitialAmmo()
    {
        playerCtrl.AmmoSystem.RifleAmmo = DataManager.Instance.CharacterDataManager.primaryWeaponData.InitialAmmo;
        playerCtrl.AmmoSystem.PistolAmmo = DataManager.Instance.CharacterDataManager.secondaryWeaponData.InitialAmmo;
    }
}
