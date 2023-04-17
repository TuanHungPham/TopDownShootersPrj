using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance;
    public static InGameManager Instance { get => instance; }

    #region public var
    public RespawnManager respawnManager;
    public GameOverManager gameOverManager;
    #endregion

    #region private var
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseSceneColor;
    #endregion

    private void Awake()
    {
        instance = this;

        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        pauseMenu = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
        pauseSceneColor = GameObject.Find("Canvas").transform.Find("PauseSceneColor").gameObject;

        respawnManager = GetComponentInChildren<RespawnManager>();
        gameOverManager = GetComponentInChildren<GameOverManager>();

        pauseMenu.SetActive(false);
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
        SceneManager.LoadScene("InGameScene");
        ResumeTime();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ResumeTime();
    }
}