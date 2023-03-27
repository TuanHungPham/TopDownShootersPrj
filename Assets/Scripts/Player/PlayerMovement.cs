using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region public var
    public float moveSpeed;
    public FixedJoystick joystick;
    #endregion

    #region private var
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Transform playerSprite;
    private Vector2 move;
    #endregion

    private void Awake()
    {
        Loadcomponents();
    }

    private void Reset()
    {
        Loadcomponents();
    }

    private void Loadcomponents()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();

        Transform uiParent = GameObject.Find("------ UI ------").transform.Find("Canvas");
        joystick = uiParent.Find("MovingJoystick").GetComponentInChildren<FixedJoystick>();

        moveSpeed = 3;
    }

    private void Update()
    {
        GetMovingDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb2d.MovePosition(rb2d.position + move * moveSpeed * Time.fixedDeltaTime);
    }

    private void GetMovingDirection()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
    }
}
