using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
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
        playerCtrl = GetComponent<PlayerCtrl>();

        maxHP = 100;
        currentHP = maxHP;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void CheckHP()
    {
        base.CheckHP();
    }

    protected override void Die()
    {
        IsDeath = true;

        playerCtrl.playerMovement.enabled = false;
        playerCtrl.playerWeapon.enabled = false;
        playerCtrl.aimingSystem.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        transform.Find("PlayerWeapon").gameObject.SetActive(false);
    }
}
