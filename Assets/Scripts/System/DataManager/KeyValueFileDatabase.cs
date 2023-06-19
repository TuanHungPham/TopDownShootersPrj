using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class KeyValueFileDatabase : IKeyValueDatabase
{
    public KeyValueFileDatabase()
    {
        ReadFile();
    }

    public T Load<T>(string key)
    {
        Debug.Log($"Data Loaded: ");
        LogSystem.LogDictionary(UserData.LoadedData);

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

    public void Save<T>(string key, T data)
    {
        string dataString = string.Empty;

        UserData.AddData(key, data);

        LogSystem.LogDictionary(UserData.LoadedData);

        SaveFile(JsonConvert.SerializeObject(UserData.LoadedData));
    }

    public void ReadFile(string filePath = "")
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = Path.Combine(Application.persistentDataPath, "data.json");
        }

        if (!File.Exists(filePath)) return;

        Debug.Log("Reading File...");

        string json = File.ReadAllText(filePath);

        Debug.Log($"JSON String: {json}");
        UserData.LoadedData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
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
