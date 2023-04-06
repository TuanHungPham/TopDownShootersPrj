using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    #region public var
    public PlayerCtrl playerCtrl;
    public EnemyMovement enemyMovement;
    public EnemyBehaviour enemyBehaviour;
    public Status enemyStatus;
    public EnemyCombat enemyCombat;
    public DamageReceiver damageReceiver;
    public ItemDropSystem itemDropSystem;
    public bool TargetExist { get => targetExist; set => targetExist = value; }
    #endregion

    #region private var
    [SerializeField] private bool targetExist;

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
        damageReceiver = GetComponent<DamageReceiver>();
        enemyStatus = GetComponent<Status>();
        enemyMovement = GetComponentInChildren<EnemyMovement>();
        enemyBehaviour = GetComponentInChildren<EnemyBehaviour>();
        enemyCombat = GetComponentInChildren<EnemyCombat>();
        itemDropSystem = GetComponentInChildren<ItemDropSystem>();
        playerCtrl = GameObject.Find("MainCharacter").GetComponent<PlayerCtrl>();
    }

    private void Update()
    {
        EnableComponents();
        CheckTargetExist();
    }

    private void EnableComponents()
    {
        if (!this.gameObject.activeSelf) return;

        enemyStatus.enabled = true;
        enemyCombat.enabled = true;
        enemyMovement.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        GameObject enemyWeapon = transform.Find("EnemySprite").GetChild(0).gameObject;
        if (enemyWeapon == null) return;
        enemyWeapon.SetActive(true);
    }

    public void DisableComponents()
    {
        enemyCombat.enabled = false;
        enemyMovement.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    public void DisableWeapon()
    {
        GameObject enemyWeapon = transform.Find("EnemySprite").GetChild(0).gameObject;
        if (enemyWeapon == null) return;
        enemyWeapon.SetActive(false);
    }

    private void CheckTargetExist()
    {
        if (playerCtrl.playerStatus.IsDeath)
        {
            targetExist = false;
            return;
        }

        targetExist = true;
    }
}
