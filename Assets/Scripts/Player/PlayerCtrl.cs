using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Space]
    public PlayerMovement playerMovement;
    public PlayerBehaviour playerBehaviour;
    public PlayerWeaponSystem playerWeaponSystem;
    public PlayerAimingSystem playerAimingSystem;
    public PlayerWeaponInventory playerWeaponInventory;
    public PlayerSwapWeaponSystem playerSwapWeaponSystem;
    public Status playerStatus;
    public GrenadeSystem grenadeSystem;
    public GrenadeAimSystem grenadeAimSystem;
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
        playerBehaviour = transform.GetComponentInChildren<PlayerBehaviour>();
        playerWeaponSystem = GetComponentInChildren<PlayerWeaponSystem>();
        playerWeaponInventory = GetComponentInChildren<PlayerWeaponInventory>();
        playerSwapWeaponSystem = GetComponentInChildren<PlayerSwapWeaponSystem>();
        ammoSystem = GetComponentInChildren<AmmoSystem>();
        grenadeAimSystem = GetComponentInChildren<GrenadeAimSystem>();
        playerAimingSystem = GameObject.Find("------ PLAYER ------").transform.Find("AimingSystem").GetComponent<PlayerAimingSystem>();
        grenadeSystem = GameObject.Find("------ PLAYER ------").transform.Find("GrenadeSystem").GetComponent<GrenadeSystem>();

        uIManager = GameObject.Find("------ UI ------").GetComponentInChildren<UIManager>();
    }

    public void EnableComponents()
    {
        playerStatus.IsDeath = false;
        playerStatus.currentHP = playerStatus.maxHP;
        playerMovement.enabled = true;
        playerBehaviour.enabled = true;
        playerWeaponSystem.enabled = true;
        playerAimingSystem.enabled = true;
        playerWeaponInventory.enabled = true;
        playerSwapWeaponSystem.enabled = true;
        grenadeAimSystem.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        uIManager.hPBarUI.hpSlider.value = playerStatus.maxHP;
    }
}
