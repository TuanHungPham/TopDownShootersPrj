using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Space]
    public PlayerMovement playerMovement;
    public PlayerWeaponSystem playerWeaponSystem;
    public PlayerAimingSystem aimingSystem;
    public PlayerShootingSystem shootingSystem;
    public PlayerWeaponInventory playerWeaponInventory;
    public Status playerStatus;
    public AmmoSystem ammoSystem;

    private void OnEnable()
    {
        playerMovement.enabled = true;
        playerWeaponSystem.enabled = true;
        aimingSystem.enabled = true;
        shootingSystem.enabled = true;
        playerWeaponInventory.enabled = true;
        playerStatus.enabled = true;
        ammoSystem.enabled = true;
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
        playerStatus = GetComponent<Status>();
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerWeaponSystem = GetComponentInChildren<PlayerWeaponSystem>();
        shootingSystem = GetComponentInChildren<PlayerShootingSystem>();
        playerWeaponInventory = GetComponentInChildren<PlayerWeaponInventory>();
        ammoSystem = GetComponentInChildren<AmmoSystem>();
        aimingSystem = GameObject.Find("------ PLAYER ------").transform.Find("AimingSystem").GetComponent<PlayerAimingSystem>();
    }
}
