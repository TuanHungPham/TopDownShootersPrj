using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverBoard;
    [SerializeField] private PlayerCtrl playerCtrl;
    private bool gameOverCheck;
    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        pauseMenu = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
        gameOverBoard = GameObject.Find("Canvas").transform.Find("GameOverBoard").gameObject;
        
        playerCtrl = GameObject.Find("------ PLAYER ------").transform.GetChild(0).GetComponent<PlayerCtrl>();

        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        SetGameOver();
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

    private void PauseTime()
    {
        Time.timeScale = 0;
    }

    private void ResumeTime()
    {
        Time.timeScale = 1;
    }

    private void SetPauseMenu()
    {
        if (Time.timeScale != 0)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(true);
        }
    }

    private void SetGameOver()
    {
        if (!playerCtrl.playerStatus.IsDeath)
        {
            gameOverCheck = false;
        }
        else
        {
            gameOverCheck = true;
            Invoke("SetGameOverBoard", 2.6f);
        }
    }

    private void SetGameOverBoard()
    {
        gameOverBoard.SetActive(gameOverCheck);

        if (!gameOverCheck) return;
        PauseTime();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("InGameScene");
        ResumeTime();
    }
}