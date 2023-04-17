using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesKilledPanel : MonoBehaviour
{
    #region public var
    public List<BoardListUI> listOfUserBoard = new List<BoardListUI>();
    #endregion

    #region private var
    [SerializeField] private Transform board;
    [SerializeField] private GameObject firstUserBoard;
    [SerializeField] private GameObject secondUserBoard;
    [SerializeField] private GameObject userBoard;
    #endregion

    private void Awake()
    {
        LoadComponents();

        IntializeBoard();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        board = transform.Find("Board");

        firstUserBoard = Resources.Load<GameObject>("Prefabs/UI/FirstPlayerListUI");
        secondUserBoard = Resources.Load<GameObject>("Prefabs/UI/SecondPlayerListUI");
        userBoard = Resources.Load<GameObject>("Prefabs/UI/PlayerListUI");
    }

    private void IntializeBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i == 0)
            {
                GameObject newUserBoard = CreateUserBoard(firstUserBoard);
                AddToList(i + 1, newUserBoard);
            }
            else if (i == 1)
            {
                GameObject newUserBoard = CreateUserBoard(secondUserBoard);
                AddToList(i + 1, newUserBoard);
            }
            else
            {
                GameObject newUserBoard = CreateUserBoard(userBoard);
                AddToList(i + 1, newUserBoard);
            }
        }
    }

    private GameObject CreateUserBoard(GameObject obj)
    {
        GameObject userBoard = Instantiate(obj);
        userBoard.transform.SetParent(board);
        userBoard.transform.localScale = Vector3.one;

        return userBoard;
    }

    private void AddToList(int index, GameObject board)
    {
        BoardListUI boardListUI = board.GetComponent<BoardListUI>();
        boardListUI.Number.text = index.ToString();
        listOfUserBoard.Add(boardListUI);
    }


}
