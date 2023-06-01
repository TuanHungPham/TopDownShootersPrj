using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private float camSpeed;
    [SerializeField] private float defaultZoom;
    [SerializeField] private float sniperZoom;
    [SerializeField] private float smoothZoom;

    [Space(20)]
    [SerializeField] private Transform player;
    [SerializeField] private Transform crosshair;
    [SerializeField] private Camera mainCam;
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private Vector3 midPoint;
    private Vector3 velocity = Vector3.zero;
    #endregion

    private void Awake()
    {
        // LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        player = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter");
        crosshair = GameObject.Find("------ PLAYER ------").transform.Find("AimingSystem").GetChild(0);

        mainCam = GetComponent<Camera>();
        playerCtrl = player.GetComponent<PlayerCtrl>();

        camSpeed = 0.75f;
        smoothZoom = 0.05f;
        defaultZoom = 10.5f;
        sniperZoom = 12f;
        mainCam.orthographicSize = defaultZoom;
    }

    private void Start()
    {
        LoadComponents();
    }

    private void Update()
    {
        Follow();
        // SetUpCameraZoom();
    }

    private void Follow()
    {
        GetMidPoint();

        transform.position = Vector3.SmoothDamp(transform.position, midPoint - new Vector3(0.5f, 0.5f, 0.5f), ref velocity, camSpeed);
    }

    private void GetMidPoint()
    {
        midPoint = (crosshair.position + player.position) / 2;
        midPoint.z = -10;
    }

    private void SetUpCameraZoom()
    {
        if (playerCtrl.PlayerWeaponSystem.SelectedWeapon.name.Equals("Sniper"))
        {
            if (mainCam.orthographicSize >= sniperZoom) return;

            mainCam.orthographicSize += smoothZoom;
            return;
        }

        if (mainCam.orthographicSize <= defaultZoom) return;
        mainCam.orthographicSize -= smoothZoom;
    }
}
