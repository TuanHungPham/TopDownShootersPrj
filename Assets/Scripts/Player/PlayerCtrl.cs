using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Space]
    public PlayerMovement playerMovement;
    public PlayerWeapon playerWeapon;
    public PlayerAimingSystem aimingSystem;
    public PlayerShootingSystem shootingSystem;

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
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerWeapon = GetComponentInChildren<PlayerWeapon>();
        shootingSystem = GetComponentInChildren<PlayerShootingSystem>();
        aimingSystem = GameObject.Find("------ PLAYER ------").transform.Find("AimingSystem").GetComponent<PlayerAimingSystem>();
    }
}
