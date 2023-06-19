using UnityEngine;

public class PlayfabSystemManager : MonoBehaviour
{
    private static PlayfabSystemManager instance;
    public static PlayfabSystemManager Instance { get => instance; set => instance = value; }

    #region public var
    public PlayfabAccountSystem PlayfabAccountSystem { get => playfabAccountSystem; set => playfabAccountSystem = value; }
    public PlayfabAchievementSystem PlayfabAchievementSystem { get => playfabAchievementSystem; set => playfabAchievementSystem = value; }
    public PlayfabLeaderboardSystem PlayfabLeaderboardSystem { get => playfabLeaderboardSystem; set => playfabLeaderboardSystem = value; }
    public PlayfabRegistrySystem PlayfabRegistrySystem { get => playfabRegistrySystem; set => playfabRegistrySystem = value; }
    public PlayfabLoginSystem PlayfabLoginSystem { get => playfabLoginSystem; set => playfabLoginSystem = value; }
    public PlayfabDatabase PlayfabDatabase { get => playfabDatabase; set => playfabDatabase = value; }
    #endregion

    #region private var
    [SerializeField] private PlayfabAccountSystem playfabAccountSystem;
    [SerializeField] private PlayfabAchievementSystem playfabAchievementSystem;
    [SerializeField] private PlayfabLeaderboardSystem playfabLeaderboardSystem;
    [SerializeField] private PlayfabRegistrySystem playfabRegistrySystem;
    [SerializeField] private PlayfabLoginSystem playfabLoginSystem;
    [SerializeField] private PlayfabDatabase playfabDatabase;
    #endregion

    private void Awake()
    {
        instance = this;

        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        playfabAccountSystem = GetComponentInChildren<PlayfabAccountSystem>();
        PlayfabAchievementSystem = GetComponentInChildren<PlayfabAchievementSystem>();
        PlayfabLeaderboardSystem = GetComponentInChildren<PlayfabLeaderboardSystem>();
        PlayfabRegistrySystem = GetComponentInChildren<PlayfabRegistrySystem>();
        PlayfabLoginSystem = GetComponentInChildren<PlayfabLoginSystem>();
    }
}
