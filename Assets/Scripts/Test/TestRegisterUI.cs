using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRegisterUI : MonoBehaviour
{
    #region public var
    public string UsernameInput { get => usernameInput; private set => usernameInput = value; }
    public string PasswordInput { get => passwordInput; private set => passwordInput = value; }
    public string EmailInput { get => emailInput; private set => emailInput = value; }
    public string DisplayNameInput { get => displayNameInput; private set => displayNameInput = value; }
    #endregion

    #region private var
    [SerializeField] private string usernameInput;
    [SerializeField] private string passwordInput;
    [SerializeField] private string emailInput;
    [SerializeField] private string displayNameInput;
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

    public void InputEmail(string inputString)
    {
        EmailInput = inputString;
    }

    public void InputDisplayName(string inputString)
    {
        DisplayNameInput = inputString;
    }

    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }
}
