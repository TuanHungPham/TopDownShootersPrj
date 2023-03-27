using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private Status objStatus;
    [SerializeField] private Animator animator;
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
        objStatus = GetComponent<Status>();
        animator = GetComponentInChildren<Animator>();
    }

    public void ReceiveDamage(int dmg)
    {
        objStatus.currentHP -= dmg;
        animator.SetTrigger("Hit");
    }
}
