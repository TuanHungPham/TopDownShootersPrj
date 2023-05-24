using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumpItemPickUp : PickUpSystem
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private int healAmount;
    [SerializeField] private PlayerCtrl playerCtrl;
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();
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
        playerCtrl = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").GetComponent<PlayerCtrl>();
    }

    private void Update()
    {
        SelfDestroy();
    }

    protected override void AddItemToPlayerInventory()
    {
        playerCtrl.playerStatus.Heal(healAmount);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void SelfDestroy()
    {
        base.SelfDestroy();
    }
}
