using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDataSceneManager : MonoBehaviour
{
    private void Start()
    {
        Invoke("LoadScene", 1f);
    }

    private void LoadScene()
    {
        if (DataManager.Instance.IsRetry)
        {
            SceneManager.LoadScene("InGameScene");
            return;
        }

        SceneManager.LoadScene("MainMenu");
    }
}
