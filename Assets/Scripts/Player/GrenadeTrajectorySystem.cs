using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTrajectorySystem : MonoBehaviour
{
    #region public var
    public float maxThrowDistance;
    public Vector3 lastPredictPosition;
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Transform grenadeDmgArea;
    private Vector2 direction;
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
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();
        grenadeDmgArea = transform.Find("GrenadeDmgAreaBG");

        Transform uiParent = GameObject.Find("------ UI ------").transform.Find("Canvas");
        joystick = uiParent.Find("GrenadeJoystick").GetComponentInChildren<FixedJoystick>();

        maxThrowDistance = 12;
    }

    private void Update()
    {
        GetGrenadeJoystickDirection();
        PredictGrenadeDmgArea();
        SetUpGrenadeDmgArea();
    }

    private void PredictGrenadeDmgArea()
    {
        grenadeDmgArea.position = Vector2.MoveTowards(grenadeDmgArea.position, direction * maxThrowDistance, 10f);
    }

    private void GetGrenadeJoystickDirection()
    {
        direction = new Vector2(joystick.Horizontal, joystick.Vertical);
    }

    private void SetUpGrenadeDmgArea()
    {
        if (direction.x != 0 || direction.y != 0)
        {
            grenadeDmgArea.gameObject.SetActive(true);
            lastPredictPosition = grenadeDmgArea.position;
            return;
        }

        grenadeDmgArea.gameObject.SetActive(false);
    }
}