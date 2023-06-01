using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        if (!CanPlay()) return;

        SceneManager.LoadScene("InGameScene");
    }

    private bool CanPlay()
    {
        CharacterDisplayCtrl characterDisplayCtrl = CharacterManagerCtrl.Instance.SelectedCharacter.GetComponent<CharacterDisplayCtrl>();

        if (characterDisplayCtrl.CharacterData.IsOwned) return true;
        return false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
