using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    #region public var
    public bool IsHit { get => isHit; }
    #endregion

    #region private var
    [SerializeField] private Status objStatus;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isHit;

    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Update()
    {
        CheckHit();
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

    private void CheckHit()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            isHit = true;
            return;
        }

        isHit = false;
    }
}
