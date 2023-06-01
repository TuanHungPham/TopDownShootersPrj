using UnityEngine;

public class GrenadeAimSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private Transform hand;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Vector2 aimDirection;
    [SerializeField] private RectTransform joystickTransform;
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
        joystickTransform = uiParent.Find("GrenadeJoystick").GetComponent<RectTransform>();
        joystick = joystickTransform.GetComponentInChildren<FixedJoystick>();
        hand = transform.Find("Hand");
    }

    private void FixedUpdate()
    {
        Aim();
    }

    private void Aim()
    {
        Vector2 joystickDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        aimDirection = joystickTransform.anchoredPosition - joystickDirection;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle - 180);
            lastRotation = transform.rotation;
            DisplayHandWithGrenade(true);
        }
        else
        {
            transform.rotation = lastRotation;
            DisplayHandWithGrenade(false);
        }
    }

    public void DisplayHandWithGrenade(bool isDisplay)
    {
        hand.gameObject.SetActive(isDisplay);
    }
}
