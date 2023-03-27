using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Status
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private EnemyCtrl enemyCtrl;
    [SerializeField] private Achievement achievement;
    #endregion

    private void OnEnable()
    {
        this.gameObject.SetActive(true);
        IsDeath = false;
        currentHP = maxHP;

        enemyCtrl.enemyCombat.enabled = true;
        enemyCtrl.enemyMovement.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.Find("EnemySprite").GetChild(0).gameObject.SetActive(true);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    private void Start()
    {
        achievement = GameObject.Find("------ OTHER ------").transform.Find("Achievement").GetComponent<Achievement>();
    }

    protected override void LoadComponents()
    {
        enemyCtrl = GetComponent<EnemyCtrl>();

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
        base.Die();
        Invoke("DisableGameObject", 2);
    }

    protected override void DisableComponents()
    {
        enemyCtrl.enemyCombat.enabled = false;
        enemyCtrl.enemyMovement.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        transform.Find("EnemySprite").GetChild(0).gameObject.SetActive(false);
    }

    private void DisableGameObject()
    {
        this.gameObject.SetActive(false);
    }
}
