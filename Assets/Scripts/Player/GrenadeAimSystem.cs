using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAimSystem : MonoBehaviour
{
    #region public var
    public FixedJoystick joystick;
    public Vector2 aimDirection;
    #endregion

    #region private var
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
        }
        else
        {
            transform.rotation = lastRotation;
        }
    }
}
