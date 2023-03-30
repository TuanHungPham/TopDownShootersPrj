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

    private void SetDeadVFX()
    {
        GameObject vfx = Instantiate(deadVFX);
        vfx.transform.position = this.transform.position;
        vfx.transform.rotation = this.transform.rotation;
    }

    protected override void DisableComponents()
    {
        enemyCtrl.DisableComponents();
    }

    private void DisableGameObject()
    {
        this.gameObject.SetActive(false);
        GetComponent<EnemyStatus>().enabled = false;
    }

    private void OnDisable()
    {
        Achievement.Instance.enemiesKilled++;
        SetDeadVFX();
    }
}
