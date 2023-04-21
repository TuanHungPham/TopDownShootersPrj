using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestNotificationUI : MonoBehaviour
{
    #region public var
    public TMP_Text NotificationText { get => notificationText; set => notificationText = value; }
    #endregion

    #region private var
    [SerializeField] private TMP_Text notificationText;
    #endregion

    private void OnEnable()
    {
        Invoke("DisablePanel", 2);
    }

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
        notificationText = GetComponentInChildren<TMP_Text>();
    }

    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    private void DisablePanel()
    {
        gameObject.SetActive(false);
    }
}
