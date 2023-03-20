using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region public var
    public float moveSpeed;
    #endregion

    #region private var
    [SerializeField] private FixedJoystick joystick;
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

        Transform player = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter");
        playerSprite = player.Find("PlayerSprite").GetComponentInChildren<Transform>();

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

        GetFaceDirection();
    }

    private void GetMovingDirection()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
    }

    private void GetFaceDirection()
    {
        Vector3 scale = playerSprite.localScale;

        if (move.x < 0)
        {
            scale.x = -0.2f;
        }
        else if(move.x > 0) 
        {
            scale.x = 0.2f;
        }

        playerSprite.localScale = scale;
    }
}
