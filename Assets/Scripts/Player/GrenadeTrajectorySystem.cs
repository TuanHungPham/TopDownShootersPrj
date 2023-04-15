using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTrajectorySystem : MonoBehaviour
{
    #region public var
    public float maxThrowDistance;
    public Vector3 lastPredictPosition;
    public GrenadeTrajectory grenadeTrajectory;
    public bool IsAreaActive { get => isAreaActive; set => isAreaActive = value; }
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private RectTransform grenadeJoystick;
    [SerializeField] private Transform grenadeDmgArea;
    [SerializeField] private Transform player;
    [SerializeField] private bool isAreaActive;
    private Vector2 distance;
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
        grenadeTrajectory = transform.Find("GrenadeTrajectory").GetComponent<GrenadeTrajectory>();
        player = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter");
        grenadeDmgArea = transform.Find("GrenadeDmgAreaBG");

        Transform uiParent = GameObject.Find("------ UI ------").transform.Find("Canvas");
        joystick = uiParent.Find("GrenadeJoystick").GetComponentInChildren<FixedJoystick>();
        grenadeJoystick = uiParent.Find("GrenadeJoystick").GetComponent<RectTransform>();

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
        Vector3 offset = direction * maxThrowDistance;
        grenadeDmgArea.position = transform.position + offset;
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
            isAreaActive = true;
            return;
        }

        isAreaActive = false;
        grenadeDmgArea.gameObject.SetActive(false);

        playerCtrl.grenadeSystem.ThrowGrenade();
        grenadeTrajectory.HideTrajectory();
        grenadeTrajectory.ClearTrajectoryPointList();
    }
}
