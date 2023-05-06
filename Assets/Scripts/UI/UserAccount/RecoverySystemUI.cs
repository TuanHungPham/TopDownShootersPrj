using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverySystemUI : MonoBehaviour
{
    public void InputEmail(string email)
    {
        PlayfabSystemManager.Instance.PlayfabAccountSystem.Email = email;
    }
}
