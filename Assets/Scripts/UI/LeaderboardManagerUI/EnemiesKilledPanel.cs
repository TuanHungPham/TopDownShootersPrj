using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesKilledPanel : LeaderboardPanel
{
    #region public var
    #endregion

    #region private var
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    protected override void IntializeBoard()
    {
        base.IntializeBoard();
    }

    protected override GameObject CreateUserBoard(GameObject obj)
    {
        return base.CreateUserBoard(obj);
    }

    protected override void AddNumerUserToList(int index, GameObject board)
    {
        base.AddNumerUserToList(index, board);
    }

    public override void SortList(List<UserManager> achievementList)
    {
        achievementList.Sort
        (
            (a, b) => b.mainAchievementData.highestEnemiesKilled.CompareTo(a.mainAchievementData.highestEnemiesKilled)
        );
    }

    public override void ShowUserAchievement(List<UserManager> achievementList)
    {
        SortList(achievementList);

        for (int i = 0; i < achievementList.Count; i++)
        {
            if (i >= listOfBoard.Count) return;

            string name = achievementList[i].userName;
            int score = achievementList[i].mainAchievementData.highestEnemiesKilled;

            listOfBoard[i].SetUIEnemiesKilledData(name, score);
        }
    }
}
