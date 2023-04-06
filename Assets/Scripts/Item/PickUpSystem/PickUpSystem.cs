using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    #endregion

    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void Reset()
    {
        LoadComponents();
    }

    protected abstract void LoadComponents();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        AddItemToPlayerInventory();
        gameObject.SetActive(false);
        Debug.Log("Da nhat item " + gameObject.name);
    }

    protected abstract void AddItemToPlayerInventory();
}
