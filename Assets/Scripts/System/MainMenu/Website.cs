using UnityEngine;

public class Website : MonoBehaviour
{
    public void GoToWebsite()
    {
        Application.OpenURL("https://www.facebook.com/profile.php?id=100093019661865");
    }

    public void GoToStore()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Kim%27s+Game");
    }
}
