using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status : MonoBehaviour
{
    #region public var
    public float currentHP;
    public float maxHP;
    public bool IsDeath { get => isDeath; set => isDeath = value; }
    #endregion

    #region private var
    [SerializeField] private bool isDeath;
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

    protected virtual void Die()
    {
        isDeath = true;
        DisableComponents();
    }

    public virtual void Heal(int healAmount)
    {
        currentHP += healAmount;

        if (currentHP < maxHP) return;

        currentHP = maxHP;
    }

    protected abstract void DisableComponents();
}
