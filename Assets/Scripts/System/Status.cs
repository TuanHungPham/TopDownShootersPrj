using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    #region public var
    public int currentHP;
    public int maxHP;
    #endregion

    #region private var
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
        maxHP = 100;
        currentHP = maxHP;
    }

    private void Update()
    {
        CheckHP();
    }

    private void CheckHP()
    {
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
    }
}
