using UnityEngine;
using TigerForge;

public class GrenadeSystem : MonoBehaviour
{
    #region public var
    public float GrenadeQuantity { get => grenadeQuantity; set => grenadeQuantity = value; }
    #endregion

    #region private var
    [SerializeField] private float grenadeQuantity;
    [SerializeField] private float throwTimer;
    [SerializeField] private float throwDelay;
    [SerializeField] private bool isThrowing;
    [SerializeField] private bool isDelay;
    [SerializeField] private bool grenadeLeft;

    [Space(20)]
    [SerializeField] private Transform throwingPoint;
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private GrenadeTrajectorySystem grenadeTrajectorySystem;
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
        grenadeTrajectorySystem = transform.parent.Find("GrenadeTrajectorySystem").GetComponent<GrenadeTrajectorySystem>();
        throwingPoint = transform.parent.Find("MainCharacter").Find("ThrowingPoint");
        grenadePrefab = Resources.Load<GameObject>("Prefabs/Grenade");

        throwDelay = 2;
        throwTimer = throwDelay;
        GrenadeQuantity = 3;
    }

    private void Update()
    {
        CheckGrenadeQuantity();
        CheckThrowTimer();
        ThrowGrenade();
    }

    public void ThrowGrenade()
    {
        if (!CanThrow() || isThrowing) return;

        GameObject grenade = Instantiate(grenadePrefab);
        grenade.transform.position = throwingPoint.position;
        grenade.transform.rotation = throwingPoint.rotation;

        ConsumeGrenade();
        isThrowing = true;
        throwTimer = throwDelay;

        EventManager.EmitEvent(EventID.PLAYER_THROWING_GRENADE.ToString());
    }

    private void CheckThrowTimer()
    {
        if (throwTimer <= 0)
        {
            isDelay = false;
            isThrowing = false;
            return;
        }

        isDelay = true;
        throwTimer -= Time.deltaTime;
    }

    private void CheckGrenadeQuantity()
    {
        if (GrenadeQuantity <= 0)
        {
            grenadeLeft = false;
            return;
        }

        grenadeLeft = true;
    }

    private bool CanThrow()
    {
        if (grenadeTrajectorySystem.GrenadeTrajectory.listOfTrajectoryPoint.Count == 0 || grenadeTrajectorySystem.IsAreaActive || isDelay || !grenadeLeft) return false;

        return true;
    }

    private void ConsumeGrenade()
    {
        GrenadeQuantity--;
    }
}
