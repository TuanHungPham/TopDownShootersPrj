using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestErrorUI : MonoBehaviour
{
    #region public var
    public TMP_Text ErrorText { get => errorText; set => errorText = value; }
    #endregion

    #region private var
    [SerializeField] private TMP_Text errorText;
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
        errorText = GetComponentInChildren<TMP_Text>();
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
