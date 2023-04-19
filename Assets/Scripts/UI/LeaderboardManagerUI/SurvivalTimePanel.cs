using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalTimePanel : LeaderboardPanel
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
            (a, b) => ComepareSurvivalTime(a, b)
        );
    }

    private int ComepareSurvivalTime(UserManager user1, UserManager user2)
    {
        if (user2.mainAchievementData.highestSurvivalTime > user1.mainAchievementData.highestSurvivalTime)
        {
            return 1;
        }
        else if (user2.mainAchievementData.highestSurvivalTime < user1.mainAchievementData.highestSurvivalTime)
        {
            return -1;
        }
        return 0;
    }

    public override void ShowUserAchievement(List<UserManager> achievementList)
    {
        SortList(achievementList);

        for (int i = 0; i < achievementList.Count; i++)
        {
            if (i >= listOfBoard.Count) return;

            string name = achievementList[i].userName;
            float time = achievementList[i].mainAchievementData.highestSurvivalTime;

            listOfBoard[i].SetUISurvivalTimeData(name, time);
        }
    }
}
