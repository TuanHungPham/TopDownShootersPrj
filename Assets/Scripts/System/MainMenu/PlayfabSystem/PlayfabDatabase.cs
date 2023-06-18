using System;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class PlayfabDatabase : IKeyValueDatabase
{
    #region public
    #endregion

    #region private
    #endregion

    public void Save<T>(string key, T data)
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        var dataString = string.Empty;
        var dataType = typeof(T);

        if (dataType.IsPrimitive || dataType == typeof(string))
        {
            dataString = data.ToString();
        }
        else
        {
            dataString = JsonConvert.SerializeObject(data);
        }

        UpdateUserDataRequest updateUserDataRequest = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {key,dataString}
            }
        };

        PlayFabClientAPI.UpdateUserData(updateUserDataRequest, OnUpdateResultCallback, OnUpdateErrorCallback);
    }

    public T Load<T>(string key)
    {
        T userData = default(T);

        if (!PlayFabClientAPI.IsClientLoggedIn()) return userData;

        GetUserDataRequest getUserDataRequest = new GetUserDataRequest();

        PlayFabClientAPI.GetUserData
        (getUserDataRequest,
        result =>
        {
            if (!result.Data.ContainsKey(key)) return;

            Debug.Log($"Data Loaded: {key}");
            Debug.Log($"Result: {result.Data[key].Value}");

            var dataType = typeof(T);

            if (dataType.IsPrimitive || dataType == typeof(string))
            {
                userData = (T)Convert.ChangeType(result.Data[key].Value, dataType);
            }
            else
            {
                userData = JsonConvert.DeserializeObject<T>(result.Data[key].Value);
            }
        }
        , OnGetErrorCallback);

        Debug.Log($"User Data: {userData}");
        return userData;
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
}
