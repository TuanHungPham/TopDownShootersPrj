using UnityEngine;

public class AttackArea : MonoBehaviour
{
    #region public var
    public bool IsTrigger { get => isTrigger; private set => isTrigger = value; }
    #endregion

    #region private var
    [SerializeField] private float triggerDistance;
    [SerializeField] private string targetTag;
    [SerializeField] private bool isTrigger;

    [Space(20)]
    [SerializeField] private Transform player;
    [SerializeField] private Transform thisEnemy;

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
        player = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter");
        thisEnemy = transform.parent.parent;

        targetTag = "Player";
    }

    private void Update()
    {
        CheckTriggerDistance();
    }

    private void CheckTriggerDistance()
    {
        float distance = Vector2.Distance(thisEnemy.position, player.position);

        if (distance > triggerDistance)
        {
            isTrigger = false;
            return;
        }

        isTrigger = true;
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag(targetTag))
    //     {
    //         IsTrigger = true;
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.CompareTag(targetTag))
    //     {
    //         IsTrigger = false;
    //     }
    // }
}
