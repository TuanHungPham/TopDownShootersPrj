using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazinePickUp : PickUpSystem
{
    #region  public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private int bulletQuantity;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void LoadComponents()
    {
        playerCtrl = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").GetComponent<PlayerCtrl>();
    }

    protected override void AddItemToPlayerInventory()
    {
        playerCtrl.ammoSystem.ammoAR += bulletQuantity;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void Reset()
    {
        base.Reset();
    }
}
