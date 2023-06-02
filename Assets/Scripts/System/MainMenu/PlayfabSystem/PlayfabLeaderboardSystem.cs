using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabLeaderboardSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    #endregion

    public void GetGlobalLeaderboard(string statsName, List<UserManager> leaderboardList)
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        GetLeaderboardAroundPlayerRequest request = new GetLeaderboardAroundPlayerRequest
        {
            MaxResultsCount = 9,
            StatisticName = statsName
        };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request,
        result =>
        {
            SetLeaderboardList(statsName, leaderboardList, result);
        }
        , OnGetLeaderboardError);
    }

    private void SetLeaderboardList(string statsName, List<UserManager> leaderboardList, GetLeaderboardAroundPlayerResult result)
    {
        foreach (var user in result.Leaderboard)
        {
            string name = user.DisplayName;
            int score = user.StatValue;

            int index = result.Leaderboard.IndexOf(user);
            leaderboardList[index].UserName = name;
            GetLeaderboardUserScore(statsName, score, leaderboardList[index]);
        }
    }

    private void GetLeaderboardUserScore(string statsName, int score, UserManager newUser)
    {
        if (statsName.Equals("EnemiesKilled"))
        {
            newUser.HighestEnemiesKilled = score;
        }
        else if (statsName.Equals("SurvivalTime"))
        {
            newUser.HighestSurvivalTime = score;
        }
    }

    private void OnGetLeaderboardError(PlayFabError error)
    {
    }
}
