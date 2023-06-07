using UnityEngine;

public class PlayerShootingSystem : MonoBehaviour
{
    #region public var
    public bool IsShooting { get => isShooting; set => isShooting = value; }
    public Weapon Weapon { get => weapon; set => weapon = value; }
    #endregion

    #region private var
    [SerializeField] private GameObject bulletTrail;
    [Space]
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private Weapon weapon;
    [SerializeField] private BulletSpawner bulletSpawner;
    [Space]
    [SerializeField] private bool isShooting;
    [SerializeField] private bool cooldown;
    #endregion

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
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();
        bulletSpawner = GameObject.Find("------ PLAYER ------").GetComponentInChildren<BulletSpawner>();

        bulletTrail = Resources.Load<GameObject>("Prefabs/BulletTrail");
    }

    private void Update()
    {
        CheckCooldown();
        Shoot();
        GetWeaponSound();
        GetShootingVFX();
    }

    private void Shoot()
    {
        if (playerCtrl.PlayerWeaponSystem.Hit.collider == null || !CanShoot())
        {
            IsShooting = false;
            return;
        }

        bulletSpawner.Spawn();

        IsShooting = true;
        playerCtrl.AmmoSystem.ConsumpAmmo();
        playerCtrl.PlayerWeaponSystem.ShootingTimer = playerCtrl.PlayerWeaponSystem.ShootingDelay;
    }

    private void GetWeaponSound()
    {
        if (playerCtrl.PlayerWeaponSystem.Hit.collider == null || !CanShoot()) return;

        playerCtrl.PlayerSound.CreateWeaponAudioSource();
    }

    private void GetShootingVFX()
    {
        if (playerCtrl.PlayerWeaponSystem.MuzzleFlash == null) return;

        if (!IsShooting) playerCtrl.PlayerWeaponSystem.MuzzleFlash.SetActive(false);
        else playerCtrl.PlayerWeaponSystem.MuzzleFlash.SetActive(true);
    }

    private void CheckCooldown()
    {
        if (playerCtrl.PlayerWeaponSystem.ShootingTimer > 0)
        {
            playerCtrl.PlayerWeaponSystem.ShootingTimer -= Time.deltaTime;
            cooldown = true;
            return;
        }

        cooldown = false;
    }

    private bool CanShoot()
    {
        if (cooldown || !playerCtrl.AmmoSystem.AmmoLeft) return false;

        return true;
    }
}
