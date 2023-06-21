using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class KeyValueFileDatabase : IKeyValueDatabase
{
    public KeyValueFileDatabase()
    {
        LoadFromDatabase();
    }

    public T GetInGameData<T>(string key)
    {
        if (UserData.LoadedData == null) return default(T);

        Debug.Log($"INGAME DATA LOADING...");
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

    public void SetInGameData<T>(string key, T data)
    {
        Debug.Log("INGAME SAVING...");
        UserData.AddData(key, data);
    }

    public void SaveToDatabase()
    {
        Debug.Log("SAVING TO FILE...");
        string content = JsonConvert.SerializeObject(UserData.LoadedData);
        SaveFile(content);
    }

    public void LoadFromDatabase(string key = null)
    {
        string json;
        ReadFile(out json);

        if (string.IsNullOrEmpty(json)) return;

        Debug.Log("LOADING FROM FILE...");
        var dataFromBase = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        UserData.LoadedData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        LogSystem.LogDictionary(UserData.LoadedData, "InGameLoadedData");
    }

    public void ReadFile(out string json, string filePath = "")
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = Path.Combine(Application.persistentDataPath, "data.json");
        }

        if (!File.Exists(filePath))
        {
            json = null;
            return;
        }

        Debug.Log("Reading File...");

        json = File.ReadAllText(filePath);
    }

    private void SaveFile(string content, string filePath = "")
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = Path.Combine(Application.persistentDataPath, "data.json");
        }

        File.WriteAllText(filePath, content);
    }
}
