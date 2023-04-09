using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : PickUpSystem
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private Weapon weapon;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void LoadComponents()
    {
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();
        weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
    }

    protected override void AddItemToPlayerInventory()
    {
        playerCtrl.playerWeaponInventory.AddWeaponToInventory(weapon.weaponData);
        playerCtrl.playerWeaponInventory.IsUpdateInventory = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
