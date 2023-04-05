using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    #region public var
    public float respawnAvailableTimer;
    public bool CanRespawn { get => canRespawn; set => canRespawn = value; }
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject respawnBoard;
    [SerializeField] private Transform enemySpawner;
    [SerializeField] private bool canRespawn;
    [SerializeField] private bool isRespawned;
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
        respawnBoard = GameObject.Find("Canvas").transform.Find("RespawnBoard").gameObject;
        playerCtrl = GameObject.Find("------ PLAYER ------").transform.GetChild(0).GetComponent<PlayerCtrl>();
        player = GameObject.Find("------ PLAYER ------").transform.GetChild(0);
        enemySpawner = GameObject.Find("------ ENEMY ------").transform.Find("EnemySpawner");

        respawnAvailableTimer = 4;
    }

    private void Update()
    {
        RespawnCheck();
        SetUpRespawnBoard();
    }

    private void SetUpRespawnBoard()
    {
        if (!canRespawn)
        {
            respawnBoard.SetActive(false);
            return;
        }

        respawnBoard.SetActive(true);
    }

    public void Respawn()
    {
        ResetPlayerComponentState();
        DespawnAllEnemy();

        isRespawned = true;
    }

    private void DespawnAllEnemy()
    {
        foreach (Transform enemy in enemySpawner)
        {
            if (!enemy.gameObject.activeSelf) continue;

            enemy.gameObject.SetActive(false);
        }
    }

    private void ResetPlayerComponentState()
    {
        playerCtrl.playerStatus.IsDeath = false;
        playerCtrl.playerStatus.currentHP = playerCtrl.playerStatus.maxHP;
        playerCtrl.playerMovement.enabled = true;
        playerCtrl.playerWeapon.enabled = true;
        playerCtrl.aimingSystem.enabled = true;
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        player.Find("PlayerWeapon").gameObject.SetActive(true);
    }

    private void RespawnCheck()
    {
        if (!playerCtrl.playerStatus.IsDeath)
        {
            canRespawn = false;
            return;
        }

        if (isRespawned || respawnAvailableTimer <= 0.1)
        {
            canRespawn = false;
            InGameManager.Instance.GameOverCheck = true;
        }
        else
        {
            canRespawn = true;
            InGameManager.Instance.GameOverCheck = false;
            RunRespawnButtonTimer();
        }
    }

    private void RunRespawnButtonTimer()
    {
        respawnAvailableTimer -= Time.deltaTime;
    }
}
