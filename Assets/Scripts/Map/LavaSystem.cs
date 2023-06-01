using System.Collections;
using UnityEngine;

public class LavaSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private int lavaDmg;
    [SerializeField] private float burnDelay;
    [SerializeField] private float burnTimer;
    [SerializeField] private bool isPlayerIn;
    [SerializeField] private bool canBurn;

    [Space(20)]
    [SerializeField] private PlayerCtrl playerCtrl;
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

        playerCtrl.DamageReceiver.ReceiveDamage(lavaDmg);

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
