using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabAchievementSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    #endregion

    private void Start()
    {
        UpdateUserAchievementToServer();
    }

    public void UpdateUserAchievementToServer()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        int highestEnemiesKilled = UserManager.Instance.HighestEnemiesKilled;
        int highestSurvivalTime = (int)UserManager.Instance.HighestSurvivalTime;

        ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
        {
            FunctionName = "UpdatePlayerAchievement",
            FunctionParameter = new
            {
                EnemiesKilled = highestEnemiesKilled,
                SurvivalTime = highestSurvivalTime
            },
            GeneratePlayStreamEvent = true
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OnCloudUpdateSuccess, OnCloudUpdateFailed);
    }

    private void OnCloudUpdateFailed(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    private void OnCloudUpdateSuccess(ExecuteCloudScriptResult result)
    {
        if (result.Error != null)
        {
            Debug.LogWarning(result.Error.Error);
            Debug.LogWarning(result.Error.Message);
            return;
        }

        Debug.Log("Update User's Achievement successfully!");
    }
}
