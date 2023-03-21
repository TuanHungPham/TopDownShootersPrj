using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Space]
    public PlayerMovement playerMovement;
    public PlayerWeapon playerWeapon;
    public PlayerAimingSystem playerAimingSystem;

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
        playerAimingSystem = GetComponentInChildren<PlayerAimingSystem>();
    }
}
