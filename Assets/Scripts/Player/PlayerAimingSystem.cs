using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimingSystem : MonoBehaviour
{
    #region public var
    public float aimDistance;
    #endregion

    #region private var
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Transform player;
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
        joystick = uiParent.Find("AimingJoystick").GetComponentInChildren<FixedJoystick>();

        player = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter");
    }

    private void FixedUpdate()
    {
        Aim();
    }

    private void Aim()
    {
        //Vector2 target = new Vector2(joystick.Horizontal, joystick.Vertical);
        //target.Normalize();
        //Transform crosshair = transform.Find("Crosshair");
        transform.position = player.position;
        //crosshair.position = target;
    }
}
