using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using TigerForge;
using System.Text;

public class PlayfabDatabase : MonoBehaviour
{
    #region public
    #endregion

    #region private
    private const string KEY = "DATA";
    // private UserData UserData;
    #endregion

    private void Awake()
    {
        // UserData = new UserData();
    }

    private void Start()
    {
        EventManager.StartListening(EventID.PLAY_GAME.ToString(), SavePlayfabData);
        EventManager.StartListening(EventID.PLAYFAB_LOGGINGIN.ToString(), LoadPlayfabData);
    }

    public void SavePlayfabData()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        Debug.LogWarning("Playfab Saving Data...");
        LogSystem.LogDictionary(UserData.LoadedData);

        var dataString = JsonConvert.SerializeObject(UserData.LoadedData);
        Debug.Log($"JSON string: {dataString}");

        UpdateUserDataRequest updateUserDataRequest = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {KEY,dataString}
            }
        };

        PlayFabClientAPI.UpdateUserData(updateUserDataRequest, OnUpdateResultCallback, OnUpdateErrorCallback);
    }

    public void LoadPlayfabData()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        Debug.LogWarning("Playfab Loading Data...");
        GetUserDataRequest getUserDataRequest = new GetUserDataRequest();

        PlayFabClientAPI.GetUserData
        (getUserDataRequest, OnGerResultCallback, OnGetErrorCallback);
    }

    private void OnGerResultCallback(GetUserDataResult result)
    {
        if (!result.Data.ContainsKey(KEY)) return;

        Debug.Log("Playfab User's Data is loaded!");

        string dataString = result.Data[KEY].Value;
        var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataString);
        LogSystem.LogDictionary(data);

        if (data == null) return;

        // UserData.LoadedData = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataString);
        UserData.LoadedData = data;

        EventManager.EmitEvent(EventID.PLAYFAB_LOADING_DATA.ToString());
    }

    private void OnGetErrorCallback(PlayFabError error)
    {
        Debug.LogWarning($"Got error getting user data !!! {error.GenerateErrorReport()}");
    }

    private void OnUpdateErrorCallback(PlayFabError error)
    {
        Debug.LogWarning($"Got error setting user data !!! {error.GenerateErrorReport()}");
    }

    private void OnUpdateResultCallback(UpdateUserDataResult result)
    {
        Debug.Log("Successfully updated user data");
    }

    private void OnApplicationQuit()
    {
        SavePlayfabData();
    }
}
