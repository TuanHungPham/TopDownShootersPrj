using UnityEngine;

public class LoginSystemUI : MonoBehaviour
{
    public void InputUsername(string username)
    {
        PlayfabSystemManager.Instance.PlayfabLoginSystem.Username = username;
    }

    public void InputPassword(string password)
    {
        PlayfabSystemManager.Instance.PlayfabLoginSystem.Password = password;
    }
}
