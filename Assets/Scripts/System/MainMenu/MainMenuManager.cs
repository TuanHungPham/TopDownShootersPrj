using System.Collections;
using System.Collections.Generic;
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
        CharacterDisplayCtrl characterDisplayCtrl = CharacterManagerCtrl.Instance.selectedCharacter.GetComponent<CharacterDisplayCtrl>();

        if (characterDisplayCtrl.characterData.IsOwned) return true;
        return false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
