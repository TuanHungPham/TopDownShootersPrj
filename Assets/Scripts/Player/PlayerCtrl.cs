using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Space]
    public PlayerMovement playerMovement;
    public PlayerWeaponSystem playerWeaponSystem;
    public PlayerAimingSystem playerAimingSystem;
    public PlayerWeaponInventory playerWeaponInventory;
    public PlayerSwapWeaponSystem playerSwapWeaponSystem;
    public Status playerStatus;
    public AmmoSystem ammoSystem;
    public DamageReceiver damageReceiver;

    public UIManager uIManager;

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
        playerStatus = GetComponent<Status>();
        damageReceiver = GetComponent<DamageReceiver>();
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerWeaponSystem = GetComponentInChildren<PlayerWeaponSystem>();
        playerWeaponInventory = GetComponentInChildren<PlayerWeaponInventory>();
        playerSwapWeaponSystem = GetComponentInChildren<PlayerSwapWeaponSystem>();
        ammoSystem = GetComponentInChildren<AmmoSystem>();
        playerAimingSystem = GameObject.Find("------ PLAYER ------").transform.Find("AimingSystem").GetComponent<PlayerAimingSystem>();

        uIManager = GameObject.Find("------ UI ------").GetComponentInChildren<UIManager>();
    }

    public void EnableComponents()
    {
        playerStatus.IsDeath = false;
        playerStatus.currentHP = playerStatus.maxHP;
        playerMovement.enabled = true;
        playerWeaponSystem.enabled = true;
        playerAimingSystem.enabled = true;
        playerWeaponInventory.enabled = true;
        playerSwapWeaponSystem.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        uIManager.hPBarUI.hpSlider.value = playerStatus.maxHP;
    }
}
