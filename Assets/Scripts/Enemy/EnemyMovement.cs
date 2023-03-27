using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region public var
    public float moveSpeed;
    #endregion

    #region private var
    [SerializeField] private Rigidbody2D rb2d;
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
        rb2d = GetComponentInParent<Rigidbody2D>();

        player = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter");
        moveSpeed = 1;
    }

    private void FixedUpdate()
    {
        ChasePlayer();
        GetFaceDirection();
    }

    private void ChasePlayer()
    {
        Vector3 direction = player.position - transform.parent.position;
        direction.Normalize();
        rb2d.MovePosition(transform.parent.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void GetFaceDirection()
    {
        Vector2 scale = transform.parent.localScale;

        if (player.position.x < transform.parent.position.x) 
        {
            scale.x = -1;
        }
        else if (player.position.x > transform.parent.position.x)
        {
            scale.x = 1;
        }

        transform.parent.localScale = scale;
    }
}
