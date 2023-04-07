using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    #region public var
    public Transform selectedWeapon;
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    private Quaternion lastRotation;
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
        GetWeaponInHolder();

        playerCtrl = GetComponentInParent<PlayerCtrl>();
    }

    private void Update()
    {
        GetWeaponInHolder();
        FlipWeapon();
        GetWeaponDirection();
    }

    private void FlipWeapon()
    {
        Vector2 scale = selectedWeapon.localScale;

        if (playerCtrl.aimingSystem.joystick.Horizontal < 0)
        {
            scale = new Vector2(-1, -1);
        }
        else if (playerCtrl.aimingSystem.joystick.Horizontal > 0)
        {
            scale = Vector2.one;
        }

        selectedWeapon.localScale = scale;
    }

    private void GetWeaponDirection()
    {
        Vector2 direction = selectedWeapon.position - playerCtrl.shootingSystem.crosshair.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (playerCtrl.aimingSystem.joystick.Horizontal != 0 && playerCtrl.aimingSystem.joystick.Vertical != 0)
        {
            selectedWeapon.rotation = Quaternion.Euler(0, 0, angle - 180);
            lastRotation = selectedWeapon.rotation;
        }
        else
        {
            selectedWeapon.rotation = lastRotation;
        }
    }

    private void GetWeaponInHolder()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf) continue;

            selectedWeapon = child.GetChild(0);
            return;
        }
    }

    private void StoreAllWeapons()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf) continue;

            child.gameObject.SetActive(false);
        }
    }
}
