using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private float existTime = 15f;
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

    protected virtual void SelfDestroy()
    {
        if (existTime <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        existTime -= Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        AddItemToPlayerInventory();
        gameObject.SetActive(false);
    }

    protected abstract void AddItemToPlayerInventory();
}
