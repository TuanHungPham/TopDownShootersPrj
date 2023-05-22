using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public class GGPGSAchievement : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    #endregion

    private void Update()
    {
        CheckGGPGSAchievement();
    }

    private void CheckGGPGSAchievement()
    {
        CheckKillAchievement();
        CheckNoobAchievement();
    }

    private void CheckKillAchievement()
    {
        switch (Achievement.Instance.enemiesKilled)
        {
            case 1:
                Social.ReportProgress(GPGSIds.achievement_first_kill, 100.0f, OnReportProgressCallback);
                break;
            case 10:
                Social.ReportProgress(GPGSIds.achievement_10_kills, 100.0f, OnReportProgressCallback);
                break;
            default:
                break;
        }
    }

    private void CheckNoobAchievement()
    {
        if (!InGameManager.Instance.gameOverManager.GameOverCheck) return;

        if (Achievement.Instance.survivalTime < 60)
        {
            Social.ReportProgress(GPGSIds.achievement_noob_like_long, 100.0f, OnReportProgressCallback);
        }
    }

    private void OnReportProgressCallback(bool success)
    {

    }
}
