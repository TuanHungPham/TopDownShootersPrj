using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    #region public var
    public int currentWeaponAmmo;
    public bool AmmoLeft { get => ammoLeft; set => ammoLeft = value; }
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private bool ammoLeft;
    [SerializeField] private int previousWeaponAmmo;
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

        currentWeaponAmmo = 200;
    }

    private void Update()
    {
        CheckAmmo();
        ConsumpAmmo();
    }

    private void ConsumpAmmo()
    {
        if (!playerCtrl.shootingSystem.IsShooting) return;

        currentWeaponAmmo--;
    }

    private void CheckAmmo()
    {
        if (currentWeaponAmmo <= 0)
        {
            ammoLeft = false;
            return;
        }

        ammoLeft = true;
    }
}
