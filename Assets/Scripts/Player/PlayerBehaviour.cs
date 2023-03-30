using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private Animator animator;
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
        animator = GetComponent<Animator>();
        playerCtrl = GetComponentInParent<PlayerCtrl>();
    }

    private void Update()
    {
        SetRunAnimation();
        SetDeathAnimation();
    }

    private void SetRunAnimation()
    {
        if (playerCtrl.playerMovement.joystick.Horizontal != 0 || playerCtrl.playerMovement.joystick.Vertical != 0 || InputManager.Instance.IsMovingInput)
        {
            animator.SetBool("Run", true);
            return;
        }

        animator.SetBool("Run", false);
    }

    private void SetDeathAnimation()
    {
        if (playerCtrl.playerStatus.IsDeath)
        {
            animator.SetBool("isDeath", true);
            return;
        }

        animator.SetBool("isDeath", false);
    }
}
