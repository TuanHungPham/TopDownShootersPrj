using UnityEngine;

public class GrenadeTrajectorySystem : MonoBehaviour
{
    #region public var
    public bool IsAreaActive { get => isAreaActive; set => isAreaActive = value; }
    public GrenadeTrajectory GrenadeTrajectory { get => grenadeTrajectory; set => grenadeTrajectory = value; }
    #endregion

    #region private var
    [SerializeField] private float maxThrowDistance;
    [SerializeField] private bool isAreaActive;

    [Space(20)]
    [SerializeField] private Vector3 lastPredictPosition;
    [SerializeField] private GrenadeTrajectory grenadeTrajectory;
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private RectTransform grenadeJoystick;
    [SerializeField] private Transform grenadeDmgArea;
    [SerializeField] private Transform player;
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
        GrenadeTrajectory = transform.Find("GrenadeTrajectory").GetComponent<GrenadeTrajectory>();
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

        GrenadeTrajectory.HideTrajectory();
    }
}
