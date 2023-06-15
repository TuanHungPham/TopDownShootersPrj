using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TigerForge;

public class MainMenuManager : MonoBehaviour
{

    #region public
    #endregion

    #region private
    [SerializeField] private Button playButton;
    #endregion

    private void Update()
    {
        HandleButton();
    }

    private void HandleButton()
    {
        if (!CanPlay())
        {
            playButton.interactable = false;
            return;
        }

        playButton.interactable = true;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("InGameScene");
        EventManager.EmitEvent(EventID.PLAY_GAME.ToString());
    }

    private bool CanPlay()
    {
        if (CharacterManagerCtrl.Instance.SelectedCharacter == null) return false;
        CharacterDisplayCtrl characterDisplayCtrl = CharacterManagerCtrl.Instance.SelectedCharacter.GetComponent<CharacterDisplayCtrl>();


        if (characterDisplayCtrl.CharacterData.IsOwned) return true;
        return false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
