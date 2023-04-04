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
    [SerializeField] private Vector3 deadPosition;
    [SerializeField] private GameObject respawnBorad;
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
        respawnBorad = GameObject.Find("Canvas").transform.Find("RespawnBoard").gameObject;
        playerCtrl = GameObject.Find("------ PLAYER ------").transform.GetChild(0).GetComponent<PlayerCtrl>();
        player = GameObject.Find("------ PLAYER ------").transform.GetChild(0);

        respawnAvailableTimer = 3;
    }

    private void Update()
    {
        RespawnCheck();
        SetUpRespawnBoard();
    }

    private void PauseTime()
    {
        Time.timeScale = 0;
    }

    private void ResumeTime()
    {
        Time.timeScale = 1;
    }

    private void SetUpRespawnBoard()
    {
        if (!canRespawn)
        {
            respawnBorad.SetActive(false);
            return;
        }

        respawnBorad.SetActive(true);
        PauseTime();
    }

    public void Respawn()
    {
        GetDeadPosition();
    }

    private void GetDeadPosition()
    {
        if (!playerCtrl.playerStatus.IsDeath) return;

        deadPosition = player.position;
    }

    private void RespawnCheck()
    {
        if (isRespawned || !playerCtrl.playerStatus.IsDeath || respawnAvailableTimer <= 0)
        {
            CanRespawn = false;
            return;
        }

        CanRespawn = true;
        RunRespawnButtonTimer();
    }

    private void RunRespawnButtonTimer()
    {
        respawnAvailableTimer -= Time.deltaTime;
    }
}
