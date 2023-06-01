using UnityEngine;
using GooglePlayGames;

public class GGPGSLeaderboard : MonoBehaviour
{
    private void Start()
    {
        PostEnemiesKillScore();
        PostTimeSurvivalScore();
    }

    public void GetEnemiesKilledLeaderboard()
    {
        GetLeaderboard(GPGSIds.leaderboard_enemies_killed);
    }

    public void GetTimeSurvivalLeaderboard()
    {
        GetLeaderboard(GPGSIds.leaderboard_time_survival);
    }

    private void GetLeaderboard(string leaderboardID)
    {
        if (!GGPGSManager.Instance.GGPGSLoginSystem.IsGGPGSLoggedIn) return;

        PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardID);
    }

    private void PostEnemiesKillScore()
    {
        Social.ReportScore(UserManager.Instance.HighestEnemiesKilled, GPGSIds.leaderboard_enemies_killed, OnReportScoreCallback);
    }

    private void PostTimeSurvivalScore()
    {
        long survivalTime = (long)UserManager.Instance.HighestSurvivalTime;
        Social.ReportScore(survivalTime, GPGSIds.leaderboard_time_survival, OnReportScoreCallback);
    }

    private void OnReportScoreCallback(bool success)
    {
    }
}
