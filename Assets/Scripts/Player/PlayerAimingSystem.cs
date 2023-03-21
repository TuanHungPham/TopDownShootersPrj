using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAimingSystem : MonoBehaviour
{
    #region public var
    public float aimDistance;
    public FixedJoystick joystick;
    #endregion

    #region private var
    [SerializeField] private RectTransform joystickTransform;
    [SerializeField] private Transform playerSprite;
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
        Transform uiParent = GameObject.Find("------ UI ------").transform.Find("Canvas");
        joystickTransform = uiParent.Find("AimingJoystick").GetComponent<RectTransform>();
        joystick = joystickTransform.GetComponentInChildren<FixedJoystick>();

        playerSprite = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").Find("PlayerSprite");
    }

    private void FixedUpdate()
    {
        Aim();
        GetFaceDirection();
    }

    private void Aim()
    {
        Vector2 joystickDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        Vector2 aimDirection = joystickTransform.anchoredPosition - joystickDirection;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle - 180);
            lastRotation = transform.rotation;
        }
        else
        {
            transform.rotation = lastRotation;
        }
    }

    private void GetFaceDirection()
    {
        Vector2 scale = playerSprite.localScale;

        if (joystick.Horizontal < 0)
        {
            scale.x = -0.2f;
        }
        else if (joystick.Horizontal > 0) 
        {
            scale.x = 0.2f;
        }

        playerSprite.localScale = scale;
    }
}
