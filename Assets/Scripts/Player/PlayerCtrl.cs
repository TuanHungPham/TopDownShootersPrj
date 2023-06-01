using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    #region public
    public PlayerMovement PlayerMovement { get => playerMovement; set => playerMovement = value; }
    public PlayerBehaviour PlayerBehaviour { get => playerBehaviour; set => playerBehaviour = value; }
    public PlayerWeaponSystem PlayerWeaponSystem { get => playerWeaponSystem; set => playerWeaponSystem = value; }
    public PlayerAimingSystem PlayerAimingSystem { get => playerAimingSystem; set => playerAimingSystem = value; }
    public PlayerWeaponInventory PlayerWeaponInventory { get => playerWeaponInventory; set => playerWeaponInventory = value; }
    public PlayerSwapWeaponSystem PlayerSwapWeaponSystem { get => playerSwapWeaponSystem; set => playerSwapWeaponSystem = value; }
    public PlayerSound PlayerSound { get => playerSound; set => playerSound = value; }
    public Status PlayerStatus { get => playerStatus; set => playerStatus = value; }
    public GrenadeSystem GrenadeSystem { get => grenadeSystem; set => grenadeSystem = value; }
    public GrenadeAimSystem GrenadeAimSystem { get => grenadeAimSystem; set => grenadeAimSystem = value; }
    public AmmoSystem AmmoSystem { get => ammoSystem; set => ammoSystem = value; }
    public DamageReceiver DamageReceiver { get => damageReceiver; set => damageReceiver = value; }
    #endregion

    #region private
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerBehaviour playerBehaviour;
    [SerializeField] private PlayerWeaponSystem playerWeaponSystem;
    [SerializeField] private PlayerAimingSystem playerAimingSystem;
    [SerializeField] private PlayerWeaponInventory playerWeaponInventory;
    [SerializeField] private PlayerSwapWeaponSystem playerSwapWeaponSystem;
    [SerializeField] private PlayerSound playerSound;
    [SerializeField] private Status playerStatus;
    [SerializeField] private GrenadeSystem grenadeSystem;
    [SerializeField] private GrenadeAimSystem grenadeAimSystem;
    [SerializeField] private AmmoSystem ammoSystem;
    [SerializeField] private DamageReceiver damageReceiver;
    #endregion

    private void OnEnable()
    {
        EnableComponents();
    }

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        PlayerStatus = GetComponent<Status>();
        DamageReceiver = GetComponent<DamageReceiver>();
        PlayerMovement = GetComponentInChildren<PlayerMovement>();
        PlayerSound = GetComponentInChildren<PlayerSound>();
        PlayerBehaviour = transform.GetComponentInChildren<PlayerBehaviour>();
        PlayerWeaponSystem = GetComponentInChildren<PlayerWeaponSystem>();
        PlayerWeaponInventory = GetComponentInChildren<PlayerWeaponInventory>();
        PlayerSwapWeaponSystem = GetComponentInChildren<PlayerSwapWeaponSystem>();
        AmmoSystem = GetComponentInChildren<AmmoSystem>();
        GrenadeAimSystem = GetComponentInChildren<GrenadeAimSystem>();
        PlayerAimingSystem = GameObject.Find("------ PLAYER ------").transform.Find("AimingSystem").GetComponent<PlayerAimingSystem>();
        GrenadeSystem = GameObject.Find("------ PLAYER ------").transform.Find("GrenadeSystem").GetComponent<GrenadeSystem>();
    }

    public void EnableComponents()
    {
        PlayerStatus.IsDeath = false;
        PlayerStatus.CurrentHP = PlayerStatus.MaxHP;
        PlayerMovement.enabled = true;
        PlayerBehaviour.enabled = true;
        PlayerWeaponSystem.enabled = true;
        PlayerAimingSystem.enabled = true;
        PlayerWeaponInventory.enabled = true;
        PlayerSwapWeaponSystem.enabled = true;
        GrenadeAimSystem.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        UIManager.Instance.HPBarUI.HpSlider.value = PlayerStatus.MaxHP;
    }
}
