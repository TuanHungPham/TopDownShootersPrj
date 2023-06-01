using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private Transform player;
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
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        transform.position = player.position;
    }
}
