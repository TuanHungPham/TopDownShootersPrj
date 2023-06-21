using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using System;
using TigerForge;

public class PlayfabDatabase : IKeyValueDatabase
{
    #region public
    #endregion

    #region private
    #endregion

    public void SetInGameData<T>(string key, T data)
    {
        Debug.Log($"INGAME DATA SAVING...");
        UserData.AddData(key, data);
    }

    public T GetInGameData<T>(string key)
    {
        Debug.Log($"INGAME DATA LOADING...");
        LogSystem.LogDictionary(UserData.LoadedData, "InGameLoadedData");

        if (UserData.LoadedData.ContainsKey(key))
        {
            string dataString = string.Empty;
            UserData.LoadedData.TryGetValue(key, out dataString);
            var datatType = typeof(T);

            if (datatType.IsPrimitive || datatType == typeof(string))
            {
                return (T)Convert.ChangeType(dataString, typeof(T));
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(dataString);
            }
        }
        else
        {
            return default(T);
        }
    }

    public void SaveToDatabase()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        Debug.Log("SAVING DATA TO PLAYFAB...");

        UpdateUserDataRequest updateUserDataRequest = new UpdateUserDataRequest
        {
            Data = UserData.LoadedData
        };

        PlayFabClientAPI.UpdateUserData(updateUserDataRequest, OnUpdateResultCallback, OnUpdateErrorCallback);
    }

    public void LoadFromDatabase(string key = null)
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        GetUserDataRequest getUserDataRequest = new GetUserDataRequest();

        PlayFabClientAPI.GetUserData(getUserDataRequest, OnGetResultCallback, OnGetErrorCallback);
    }

    private void OnGetErrorCallback(PlayFabError error)
    {
        Debug.LogWarning($"Got error getting user data !!! {error.GenerateErrorReport()}");
    }

    private void OnGetResultCallback(GetUserDataResult result)
    {
        if (result.Data == null) return;

        Debug.Log("LOADING DATA FROM PLAYFAB...");

        foreach (var playfabData in result.Data)
        {
            string dataString = playfabData.Value.Value;
            UserData.AddData(playfabData.Key, dataString);
        }

        EventManager.EmitEvent(EventID.PLAYFAB_LOADING_DATA.ToString());
    }

    private void OnUpdateErrorCallback(PlayFabError error)
    {
        Debug.LogWarning($"Got error setting user data !!! {error.GenerateErrorReport()}");
    }

    private void OnUpdateResultCallback(UpdateUserDataResult result)
    {
        Debug.Log("Successfully updated user data");
    }
}
