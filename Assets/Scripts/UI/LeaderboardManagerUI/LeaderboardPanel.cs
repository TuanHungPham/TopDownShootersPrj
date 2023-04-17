using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LeaderboardPanel : MonoBehaviour
{
    #region public var
    public List<BoardUI> listOfBoard = new List<BoardUI>();
    #endregion

    #region private var
    [SerializeField] protected Transform board;
    [SerializeField] protected GameObject firstUserBoard;
    [SerializeField] protected GameObject secondUserBoard;
    [SerializeField] protected GameObject userBoard;
    #endregion

    protected virtual void Awake()
    {
        LoadComponents();

        IntializeBoard();
    }

    protected virtual void Reset()
    {
        LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        board = transform.Find("Board");

        firstUserBoard = Resources.Load<GameObject>("Prefabs/UI/FirstPlayerListUI");
        secondUserBoard = Resources.Load<GameObject>("Prefabs/UI/SecondPlayerListUI");
        userBoard = Resources.Load<GameObject>("Prefabs/UI/PlayerListUI");
    }

    public void DisableBoard()
    {
        this.gameObject.SetActive(false);
    }

    protected virtual void IntializeBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i == 0)
            {
                GameObject newUserBoard = CreateUserBoard(firstUserBoard);
                AddNumerUserToList(i + 1, newUserBoard);
            }
            else if (i == 1)
            {
                GameObject newUserBoard = CreateUserBoard(secondUserBoard);
                AddNumerUserToList(i + 1, newUserBoard);
            }
            else
            {
                GameObject newUserBoard = CreateUserBoard(userBoard);
                AddNumerUserToList(i + 1, newUserBoard);
            }
        }
    }

    protected virtual GameObject CreateUserBoard(GameObject obj)
    {
        GameObject userBoard = Instantiate(obj);
        userBoard.transform.SetParent(board);
        userBoard.transform.localScale = Vector3.one;

        return userBoard;
    }

    protected virtual void AddNumerUserToList(int index, GameObject board)
    {
        BoardUI boardListUI = board.GetComponent<BoardUI>();
        boardListUI.Number.text = index.ToString();
        listOfBoard.Add(boardListUI);
    }

    public abstract void SortList(List<UserManager> achievementList);

    public abstract void ShowUserAchievement(List<UserManager> achievementList);

    //Kiem tra user ton tai chua?
    //Neu user ton tai thi thay highscore
}
