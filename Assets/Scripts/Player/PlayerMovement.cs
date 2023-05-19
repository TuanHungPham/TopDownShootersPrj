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
    [SerializeField] private PlayerCtrl playerCtrl;
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
        playerCtrl = transform.parent.GetComponent<PlayerCtrl>();

        Transform uiParent = GameObject.Find("------ UI ------").transform.Find("Canvas");
        joystick = uiParent.Find("MovingJoystick").GetComponentInChildren<FixedJoystick>();

        moveSpeed = 3;
    }

    private void Update()
    {
        SetMovingDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void SetMovingDirection()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            move.x = Input.GetAxis("Horizontal");
            move.y = Input.GetAxis("Vertical");
            return;
        }

        GetMovingDirectionByTouchPad();
    }

    private void Move()
    {
        rb2d.MovePosition(rb2d.position + move * moveSpeed * Time.fixedDeltaTime);
    }

    private void GetMovingDirectionByTouchPad()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
    }
}
