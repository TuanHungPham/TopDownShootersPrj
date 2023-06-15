using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance;
    public static InGameManager Instance { get => instance; }
    public RespawnManager RespawnManager { get => respawnManager; set => respawnManager = value; }
    public GameOverManager GameOverManager { get => gameOverManager; set => gameOverManager = value; }

    #region public var
    #endregion

    #region private var
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseSceneColor;
    [SerializeField] private RespawnManager respawnManager;
    [SerializeField] private GameOverManager gameOverManager;
    #endregion

    private void Awake()
    {
        instance = this;

        LoadComponents();

        DataManager.Instance.IsRetry = false;
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        pauseMenu = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
        pauseSceneColor = GameObject.Find("Canvas").transform.Find("PauseSceneColor").gameObject;

        RespawnManager = GetComponentInChildren<RespawnManager>();
        GameOverManager = GetComponentInChildren<GameOverManager>();
    }

    public void SetPauseAndResumeGame()
    {
        if (Time.timeScale != 0)
        {
            PauseTime();
        }
        else
        {
            ResumeTime();
        }

        SetPauseMenu();
    }

    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
    }

    private void SetPauseMenu()
    {
        if (Time.timeScale != 0)
        {
            pauseMenu.SetActive(false);
            pauseSceneColor.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(true);
            pauseSceneColor.SetActive(true);
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("LoadDataScene");
        DataManager.Instance.IsRetry = true;
        ResumeTime();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ResumeTime();
    }
}