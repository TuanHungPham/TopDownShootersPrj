using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
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
        playerCtrl = GetComponentInParent<PlayerCtrl>();
    }

    private void Update()
    {
        FlipWeapon();
    }

    private void FlipWeapon()
    {
        Vector2 scale = transform.localScale;

        if (playerCtrl.playerAimingSystem.joystick.Horizontal < 0)
        {
            scale.x = -1;
        }
        else if (playerCtrl.playerAimingSystem.joystick.Horizontal > 0)
        {
            scale.x = 1;
        }

        transform.localScale = scale;
    }
}
