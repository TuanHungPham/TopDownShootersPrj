using UnityEngine;
using MarchingBytes;

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
        CurrentHP = MaxHP;
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

        CurrentHP = MaxHP;
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
        Invoke("DisableGameObject", 1.2f);
    }

    protected override void DisableComponents()
    {
        enemyCtrl.DisableComponents();
    }

    private void DisableGameObject()
    {
        EasyObjectPool.instance.ReturnObjectToPool(this.gameObject);
        enemyCtrl.DisableWeapon();
        GetComponent<EnemyStatus>().enabled = false;
    }
}
