using UnityEngine;

public class PlayerWeaponSystem : MonoBehaviour
{
    #region public var
    public Transform SelectedWeapon { get => selectedWeapon; set => selectedWeapon = value; }
    public Transform ShootingPoint { get => shootingPoint; set => shootingPoint = value; }
    public Transform Crosshair { get => crosshair; set => crosshair = value; }
    public float ShootDistance { get => shootDistance; set => shootDistance = value; }
    public float ShootingTimer { get => shootingTimer; set => shootingTimer = value; }
    public float ShootingDelay { get => shootingDelay; set => shootingDelay = value; }
    public int Dmg { get => dmg; set => dmg = value; }
    public GameObject MuzzleFlash { get => muzzleFlash; set => muzzleFlash = value; }
    public PlayerShootingSystem PlayerShootingSystem { get => playerShootingSystem; set => playerShootingSystem = value; }
    public RaycastHit2D Hit { get => hit; set => hit = value; }
    public PlayerCtrl PlayerCtrl { get => playerCtrl; set => playerCtrl = value; }
    public LayerMask EnemyLayer { get => enemyLayer; set => enemyLayer = value; }
    #endregion

    #region private var
    [Space]
    [SerializeField] private Transform selectedWeapon;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform crosshair;

    [Space]
    [SerializeField] private float shootDistance;
    [SerializeField] private float shootingTimer;
    [SerializeField] private float shootingDelay;
    [SerializeField] private int dmg;
    [SerializeField] private GameObject muzzleFlash;

    [Space]
    [SerializeField] private PlayerShootingSystem playerShootingSystem;
    [SerializeField] private RaycastHit2D hit;
    [SerializeField] private PlayerCtrl playerCtrl;

    [Space]
    [SerializeField] private LayerMask enemyLayer;
    private Vector3 direction;
    private Quaternion lastRotation;
    private Vector2 lastScale;
    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Start()
    {
        lastScale = Vector2.one;
    }

    private void LoadComponents()
    {
        PlayerCtrl = GetComponentInParent<PlayerCtrl>();

        Crosshair = GameObject.Find("------ PLAYER ------").transform.Find("AimingSystem").GetChild(0);

        EnemyLayer = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        GetWeaponInHolder();

        FlipWeapon();
        GetWeaponDirection();

        GetShootDirection();
        SetUpAimingLine();
    }

    private void SetUpAimingLine()
    {
        ShootDistance = PlayerShootingSystem.Weapon.WeaponData.ShootDistance;

        Hit = Physics2D.Raycast(ShootingPoint.position, direction, ShootDistance, EnemyLayer);
        Debug.DrawRay(ShootingPoint.position, direction * ShootDistance, Color.red);
        SetUpCrosshairPosition();
    }

    private void GetShootDirection()
    {
        direction = Crosshair.position - ShootingPoint.position;
        direction.Normalize();
    }

    private void SetUpCrosshairPosition()
    {
        Crosshair.position = ShootingPoint.position + direction * ShootDistance;
    }

    private void FlipWeapon()
    {
        FlipNewWeapon();

        Vector2 scale = SelectedWeapon.localScale;

        if (PlayerCtrl.PlayerAimingSystem.Joystick.Horizontal < 0)
        {
            scale = new Vector2(-1, -1);
        }
        else if (PlayerCtrl.PlayerAimingSystem.Joystick.Horizontal > 0)
        {
            scale = Vector2.one;
        }

        lastScale = scale;
        SelectedWeapon.localScale = scale;
    }

    private void FlipNewWeapon()
    {
        if (!UIManager.Instance.WeaponInventoryPanel.IsWeaponSwitched) return;

        SelectedWeapon.localScale = lastScale;
    }

    private void GetWeaponDirection()
    {
        Vector2 direction = SelectedWeapon.position - Crosshair.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (PlayerCtrl.PlayerAimingSystem.Joystick.Horizontal != 0 && PlayerCtrl.PlayerAimingSystem.Joystick.Vertical != 0)
        {
            SelectedWeapon.rotation = Quaternion.Euler(0, 0, angle - 180);
            lastRotation = SelectedWeapon.rotation;
        }
        else
        {
            SelectedWeapon.rotation = lastRotation;
        }
    }

    public void GetWeaponInHolder()
    {
        foreach (Transform child in transform)
        {
            Transform holder = child.Find("Holder");
            if (!holder.gameObject.activeSelf) continue;

            if (holder.childCount <= 0) continue;
            SelectedWeapon = child.Find("Holder").GetChild(0);

            ShootingPoint = SelectedWeapon.Find("ShootingPoint");

            PlayerShootingSystem = child.GetComponent<PlayerShootingSystem>();
            GetWeaponInfo(PlayerShootingSystem, child);
            return;
        }
    }

    private void GetWeaponInfo(PlayerShootingSystem playerShootingSystem, Transform transform)
    {
        MuzzleFlash = SelectedWeapon.Find("Muzzle").gameObject;

        playerShootingSystem.Weapon = transform.Find("Holder").GetComponentInChildren<Weapon>();
        ShootDistance = playerShootingSystem.Weapon.WeaponData.ShootDistance;
        ShootingDelay = 1 / playerShootingSystem.Weapon.WeaponData.FireRate;
        Dmg = playerShootingSystem.Weapon.WeaponData.WeaponDmg;
    }
}
