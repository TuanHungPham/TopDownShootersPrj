using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabSystemManager : MonoBehaviour
{
    private static PlayfabSystemManager instance;
    public static PlayfabSystemManager Instance { get => instance; set => instance = value; }

    #region public var
    public PlayfabAccountSystem PlayfabAccountSystem { get => playfabAccountSystem; set => playfabAccountSystem = value; }
    public PlayfabAchievementSystem PlayfabAchievementSystem { get => playfabAchievementSystem; set => playfabAchievementSystem = value; }
    public PlayfabLeaderboardSystem PlayfabLeaderboardSystem { get => playfabLeaderboardSystem; set => playfabLeaderboardSystem = value; }
    #endregion

    #region private var
    [SerializeField] private PlayfabAccountSystem playfabAccountSystem;
    [SerializeField] private PlayfabAchievementSystem playfabAchievementSystem;
    [SerializeField] private PlayfabLeaderboardSystem playfabLeaderboardSystem;
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
    }
}
