using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Status
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private EnemyCtrl enemyCtrl;
    [SerializeField] private GameObject deadVFX;
    #endregion

    private void OnEnable()
    {
        IsDeath = false;
        currentHP = maxHP;
    }

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
        enemyCtrl = GetComponent<EnemyCtrl>();

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
        base.Die();
        Invoke("DisableGameObject", 2.6f);
    }

    protected override void DisableComponents()
    {
        enemyCtrl.DisableComponents();
    }

    private void DisableGameObject()
    {
        this.gameObject.SetActive(false);
        enemyCtrl.DisableWeapon();
        GetComponent<EnemyStatus>().enabled = false;
    }
}
