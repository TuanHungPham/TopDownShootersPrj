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
        Debug.Log($"JSON string: {json}");
        // if (Application.platform == RuntimePlatform.Android)
        // {
        //     List<Dictionary<string, string>> list = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
        //     Debug.Log($"LIST: {list[0]}");
        //     foreach (var dictionary in list)
        //     {
        //         UserData.LoadedData.Add(dictionary.Keys.ToString(), dictionary.Values.ToString());
        //     }
        // }
        // else
        // {
        //     UserData.LoadedData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        //     LogSystem.LogDictionary(UserData.LoadedData, "InGameLoadedData");
        // }

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
            Debug.LogWarning("File is not exist or cannot find this File!!!");
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
