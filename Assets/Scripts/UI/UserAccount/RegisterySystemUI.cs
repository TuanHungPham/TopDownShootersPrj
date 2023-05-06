using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterySystemUI : MonoBehaviour
{
    public void InputUsername(string username)
    {
        PlayfabSystemManager.Instance.PlayfabRegistrySystem.Username = username;
    }

    public void InputPassword(string password)
    {
        PlayfabSystemManager.Instance.PlayfabRegistrySystem.Password = password;
    }

    public void ReEnterPassword(string password)
    {
        PlayfabSystemManager.Instance.PlayfabRegistrySystem.ReEnterPassword = password;
    }

    public void InputEmail(string email)
    {
        PlayfabSystemManager.Instance.PlayfabRegistrySystem.Email = email;
    }
}
