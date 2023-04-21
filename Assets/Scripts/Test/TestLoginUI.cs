using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestLoginUI : MonoBehaviour
{
    #region public var
    public string UsernameInput { get => usernameInput; set => usernameInput = value; }
    public string PasswordInput { get => passwordInput; set => passwordInput = value; }
    #endregion

    #region private var
    [SerializeField] private string usernameInput;
    [SerializeField] private string passwordInput;

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
    }

    public void InputUsername(string inputString)
    {
        UsernameInput = inputString;
    }

    public void InputPassword(string inputString)
    {
        PasswordInput = inputString;
    }

    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }
}
