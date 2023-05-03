using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabSystemManager : MonoBehaviour
{
    private static PlayfabSystemManager instance;
    public static PlayfabSystemManager Instance { get => instance; set => instance = value; }

    #region public var
    public PlayfabAccountSystem PlayfabAccountSystem { get => playfabAccountSystem; set => playfabAccountSystem = value; }
    #endregion

    #region private var
    [SerializeField] private PlayfabAccountSystem playfabAccountSystem;
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
    }
}
