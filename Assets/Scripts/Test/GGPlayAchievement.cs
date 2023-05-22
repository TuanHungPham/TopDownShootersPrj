using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public class GGPlayAchievement : MonoBehaviour
{
    public string testAchievementID;

    private void Awake()
    {
        testAchievementID = "CgkI-eWTwLYHEAIQAQ";
    }

    private void Update()
    {
        CheckGGPlayAchievement();
    }

    private void CheckGGPlayAchievement()
    {
        if (TestScore.Instance.score < 5 || TestScore.Instance.score > 5) return;
        Social.ReportProgress(testAchievementID, 100.0, OnReportProgressCallback);
    }

    private void OnReportProgressCallback(bool success)
    {
        if (success)
        {
            Debug.Log("You've just unlocked Achievement!!!");
        }
        else
        {
            Debug.Log("Achievement System FAILED!!!");
        }
    }
}
