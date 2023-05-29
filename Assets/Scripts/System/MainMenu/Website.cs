using UnityEngine;

public class Website : MonoBehaviour
{
    public void GoToWebsite()
    {
        Application.OpenURL("https://www.facebook.com/profile.php?id=100093019661865");
    }
}
