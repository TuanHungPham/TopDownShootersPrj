using UnityEngine;

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
        switch (Achievement.Instance.EnemiesKilled)
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
        if (!InGameManager.Instance.GameOverManager.GameOverCheck) return;

        if (Achievement.Instance.SurvivalTime < 60)
        {
            Social.ReportProgress(GPGSIds.achievement_noob_like_long, 100.0f, OnReportProgressCallback);
        }
    }

    private void OnReportProgressCallback(bool success)
    {

    }
}
