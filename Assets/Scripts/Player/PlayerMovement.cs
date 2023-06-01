using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region public var
    public FixedJoystick Joystick { get => joystick; set => joystick = value; }
    #endregion

    #region private var
    [SerializeField] private float moveSpeed;

    [Space(20)]
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private PlayerCtrl playerCtrl;
    private Vector2 direction;
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
        Joystick = uiParent.Find("MovingJoystick").GetComponentInChildren<FixedJoystick>();

        moveSpeed = 3.5f;
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
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");
            return;
        }

        GetMovingDirectionByTouchPad();
    }

    private void Move()
    {
        rb2d.MovePosition(rb2d.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void GetMovingDirectionByTouchPad()
    {
        direction.x = Joystick.Horizontal;
        direction.y = Joystick.Vertical;
    }
}
