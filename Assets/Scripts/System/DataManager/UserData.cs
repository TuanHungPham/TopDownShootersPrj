using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public static class UserData
{
    #region public
    public static Dictionary<string, string> LoadedData
    {
        get => loadedData;
        set
        {
            loadedData = value;
            // Debug.Log($"Value: {JsonConvert.SerializeObject(value)}");
        }
    }
    public static Dictionary<string, string> ModifiedData { get => modifiedData; set => modifiedData = value; }
    #endregion

    #region private
    private static Dictionary<string, string> loadedData = new Dictionary<string, string>();
    private static Dictionary<string, string> modifiedData = new Dictionary<string, string>();
    #endregion

    static UserData()
    {
    }

    public static void AddData<T>(string key, T data)
    {
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

        loadedData[key] = dataString;

        // if (!loadedData.ContainsKey(key) || !loadedData[key].Equals(dataString))
        // {
        //     modifiedData[key] = dataString;
        //     LogSystem.LogDictionary(modifiedData, "ModifiedData");
        // }
    }
}
