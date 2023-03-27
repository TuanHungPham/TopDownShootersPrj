using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status : MonoBehaviour
{
    #region public var
    public int currentHP;
    public int maxHP;
    public bool IsDeath { get => isDeath; protected set => isDeath = value; }
    #endregion

    #region private var
    private bool isDeath;
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

    protected virtual void Update()
    {
        CheckHP();
    }

    protected virtual void CheckHP()
    {
        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
