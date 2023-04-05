using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    #region public var
    public bool GameOverCheck { get => gameOverCheck; set => gameOverCheck = value; }
    #endregion

    #region private var
    [SerializeField] private GameObject gameOverBoard;
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

    private void Update()
    {
        GetGameOverBoard();
    }

    private void LoadComponents()
    {
        gameOverBoard = GameObject.Find("Canvas").transform.Find("GameOverBoard").gameObject;
    }

    private void GetGameOverBoard()
    {
        if (!GameOverCheck) return;

        Invoke("SetGameOverBoard", 2.6f);
    }

    private void SetGameOverBoard()
    {
        gameOverBoard.SetActive(GameOverCheck);

        if (!GameOverCheck) return;
        InGameManager.Instance.PauseTime();
    }
}
