using UnityEngine;

public class PlayerAimingSystem : MonoBehaviour
{
    #region public var
    public FixedJoystick Joystick { get => joystick; set => joystick = value; }
    #endregion

    #region private var
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Vector2 aimDirection;
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
        Joystick = joystickTransform.GetComponentInChildren<FixedJoystick>();

        playerSprite = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter");
    }

    private void FixedUpdate()
    {
        Aim();
        GetFaceDirection();
    }

    private void Aim()
    {
        Vector2 joystickDirection = new Vector2(Joystick.Horizontal, Joystick.Vertical);
        aimDirection = joystickTransform.anchoredPosition - joystickDirection;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (Joystick.Horizontal != 0 && Joystick.Vertical != 0)
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

        if (Joystick.Horizontal < 0)
        {
            scale.x = -1f;
        }
        else if (Joystick.Horizontal > 0)
        {
            scale.x = 1f;
        }

        playerSprite.localScale = scale;
    }
}
