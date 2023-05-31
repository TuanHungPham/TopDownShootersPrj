using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (playerCtrl.playerWeaponSystem.hit.collider == null || !CanShoot())
        {
            IsShooting = false;
            return;
        }

        bulletSpawner.Spawn(playerCtrl.playerWeaponSystem.hit.collider.transform, playerCtrl.playerWeaponSystem.dmg);

        IsShooting = true;
        playerCtrl.ammoSystem.ConsumpAmmo();
        playerCtrl.playerWeaponSystem.shootingTimer = playerCtrl.playerWeaponSystem.shootingDelay;
    }

    private void GetWeaponSound()
    {
        if (playerCtrl.playerWeaponSystem.hit.collider == null || !CanShoot()) return;

        playerCtrl.playerSound.CreateWeaponAudioSource();
    }

    private void GetShootingVFX()
    {
        if (playerCtrl.playerWeaponSystem.muzzleFlash == null) return;

        if (!IsShooting) playerCtrl.playerWeaponSystem.muzzleFlash.SetActive(false);
        else playerCtrl.playerWeaponSystem.muzzleFlash.SetActive(true);
    }

    private void CheckCooldown()
    {
        if (playerCtrl.playerWeaponSystem.shootingTimer > 0)
        {
            playerCtrl.playerWeaponSystem.shootingTimer -= Time.deltaTime;
            cooldown = true;
            return;
        }

        cooldown = false;
    }

    private bool CanShoot()
    {
        if (cooldown || !playerCtrl.ammoSystem.AmmoLeft) return false;

        return true;
    }
}
