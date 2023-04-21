using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIManager : MonoBehaviour
{
    private static TestUIManager instance;
    public static TestUIManager Instance { get => instance; set => instance = value; }

    #region public var
    public TestLoginUI TestLoginUI { get => testLoginUI; set => testLoginUI = value; }
    public TestRegisterUI TestRegisterUI { get => testRegisterUI; set => testRegisterUI = value; }
    public TestErrorUI TestErrorUI { get => testErrorUI; set => testErrorUI = value; }
    public TestNotificationUI TestNotificationUI { get => testNotificationUI; set => testNotificationUI = value; }
    #endregion

    #region private var
    [SerializeField] private TestLoginUI testLoginUI;
    [SerializeField] private TestRegisterUI testRegisterUI;
    [SerializeField] private TestErrorUI testErrorUI;
    [SerializeField] private TestNotificationUI testNotificationUI;

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
        TestLoginUI = transform.Find("LoginPanel").GetComponent<TestLoginUI>();
        TestRegisterUI = transform.Find("SignUpPanel").GetComponent<TestRegisterUI>();
        TestErrorUI = transform.Find("ErrorPanel").GetComponent<TestErrorUI>();
        TestNotificationUI = transform.Find("NotificationPanel").GetComponent<TestNotificationUI>();
    }
}
