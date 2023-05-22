using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGPGSManager : MonoBehaviour
{
    private static GGPGSManager instance;
    public static GGPGSManager Instance { get => instance; private set => instance = value; }

    #region public var
    public GGPGSLoginSystem GGPGSLoginSystem { get => gGPGSLoginSystem; private set => gGPGSLoginSystem = value; }
    public GGPGSLeaderboard GGPGSLeaderboard { get => gGPGSLeaderboard; private set => gGPGSLeaderboard = value; }
    #endregion

    #region private var
    [SerializeField] private GGPGSLoginSystem gGPGSLoginSystem;
    [SerializeField] private GGPGSLeaderboard gGPGSLeaderboard;

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
        GGPGSLoginSystem = GetComponentInChildren<GGPGSLoginSystem>();
        GGPGSLeaderboard = GetComponentInChildren<GGPGSLeaderboard>();
    }

}
