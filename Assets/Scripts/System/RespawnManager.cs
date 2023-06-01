using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    #region public var
    public bool CanRespawn { get => canRespawn; set => canRespawn = value; }
    public bool IsRespawned { get => isRespawned; set => isRespawned = value; }
    public bool IsRespawning { get => isRespawning; set => isRespawning = value; }
    public float RespawnAvailableTimer { get => respawnAvailableTimer; set => respawnAvailableTimer = value; }
    #endregion

    #region private var
    [SerializeField] private float respawnAvailableTimer;
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject respawnBoard;
    [SerializeField] private Transform enemySpawner;
    [SerializeField] private bool canRespawn;
    [SerializeField] private bool isRespawned;
    [SerializeField] private bool isRespawning;
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

        RespawnAvailableTimer = 6.5f;
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
        isRespawning = true;
        isRespawned = true;
        Invoke("ResetPlayerComponentState", 1.5f);
    }

    private void ResetPlayerComponentState()
    {
        EnableWeapon();
        playerCtrl.EnableComponents();

        isRespawning = false;
    }

    private void RespawnCheck()
    {
        if (!playerCtrl.PlayerStatus.IsDeath || (isRespawned && isRespawning))
        {
            canRespawn = false;
            InGameManager.Instance.GameOverManager.GameOverCheck = false;
            return;
        }

        if ((isRespawned || RespawnAvailableTimer <= 0.1) && !isRespawning)
        {
            canRespawn = false;
            InGameManager.Instance.GameOverManager.GameOverCheck = true;
        }
        else
        {
            canRespawn = true;
            InGameManager.Instance.GameOverManager.GameOverCheck = false;
            RunRespawnButtonTimer();
        }
    }

    private void RunRespawnButtonTimer()
    {
        RespawnAvailableTimer -= Time.deltaTime;
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
