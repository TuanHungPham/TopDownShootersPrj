using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    #region public var
    public float respawnAvailableTimer;
    public bool CanRespawn { get => canRespawn; set => canRespawn = value; }
    public bool IsRespawned { get => isRespawned; set => isRespawned = value; }
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
        playerCtrl = GameObject.Find("------ PLAYER ------").transform.GetChild(0).GetComponent<PlayerCtrl>();

        respawnBoard = GameObject.Find("Canvas").transform.Find("RespawnBoard").gameObject;
        player = GameObject.Find("------ PLAYER ------").transform.GetChild(0);
        enemySpawner = GameObject.Find("------ ENEMY ------").transform.Find("EnemySpawner");

        respawnAvailableTimer = 6.5f;
    }

    private void Update()
    {
        RespawnCheck();
        GetRespawnBoard();
    }

    private void SetRespawnBoard()
    {
        respawnBoard.SetActive(canRespawn);
    }

    private void GetRespawnBoard()
    {
        if (!canRespawn)
        {
            SetRespawnBoard();
            return;
        }

        Invoke("SetRespawnBoard", 1.8f);
    }

    public void Respawn()
    {
        ResetPlayerComponentState();

        IsRespawned = true;
    }

    private void ResetPlayerComponentState()
    {
        EnableWeapon();

        playerCtrl.EnableComponents();
    }

    private void RespawnCheck()
    {
        if (!playerCtrl.playerStatus.IsDeath)
        {
            canRespawn = false;
            return;
        }

        if (IsRespawned || respawnAvailableTimer <= 0.1)
        {
            canRespawn = false;
            InGameManager.Instance.gameOverManager.GameOverCheck = true;
        }
        else
        {
            canRespawn = true;
            InGameManager.Instance.gameOverManager.GameOverCheck = false;
            RunRespawnButtonTimer();
        }
    }

    private void RunRespawnButtonTimer()
    {
        respawnAvailableTimer -= Time.deltaTime;
    }

    private void EnableWeapon()
    {
        foreach (Transform item in GameObject.Find("------ PLAYER ------").transform.GetChild(0).Find("PlayerWeapon"))
        {
            if (item.gameObject.activeSelf) continue;

            item.gameObject.SetActive(true);
        }
    }
}
