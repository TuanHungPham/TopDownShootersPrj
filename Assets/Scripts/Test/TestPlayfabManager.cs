using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayfabManager : MonoBehaviour
{
    private static TestPlayfabManager instance;
    public static TestPlayfabManager Instance { get => instance; set => instance = value; }

    #region public var
    public TestPlayfabLogin PlayfabLogin { get => playfabLogin; set => playfabLogin = value; }
    public TestPlayfabRegister PlayfabRegister { get => playfabRegister; set => playfabRegister = value; }
    #endregion

    #region private var
    [SerializeField] private TestPlayfabLogin playfabLogin;
    [SerializeField] private TestPlayfabRegister playfabRegister;
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
        playfabLogin = GetComponentInChildren<TestPlayfabLogin>();
        playfabRegister = GetComponentInChildren<TestPlayfabRegister>();
    }
}
