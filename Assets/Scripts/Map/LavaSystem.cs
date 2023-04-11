using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSystem : MonoBehaviour
{
    #region public var
    public int lavaDmg;
    public float burnDelay;
    public float burnTimer;
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private bool isPlayerIn;
    [SerializeField] private bool canBurn;
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
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();

        burnDelay = 1;
        burnTimer = 0;
    }

    private void Update()
    {
        CheckBurnTimer();
        Burn();
    }

    private void Burn()
    {
        if (!isPlayerIn || !canBurn) return;

        playerCtrl.damageReceiver.ReceiveDamage(lavaDmg);

        burnTimer = burnDelay;
    }

    private void CheckBurnTimer()
    {
        if (burnTimer <= 0)
        {
            canBurn = true;
            return;
        }

        burnTimer -= Time.deltaTime;
        canBurn = false;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (!collider2D.CompareTag("Player")) return;

        isPlayerIn = true;
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (!collider2D.CompareTag("Player")) return;

        isPlayerIn = false;
    }
}
