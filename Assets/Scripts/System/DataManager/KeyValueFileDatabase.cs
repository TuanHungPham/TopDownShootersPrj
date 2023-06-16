using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class KeyValueFileDatabase : IKeyValueDatabase
{
    private Data _data;

    public KeyValueFileDatabase(Dictionary<string, string> collection)
    {
        _data = new Data(collection);
        ReadFile();
    }

    public KeyValueFileDatabase()
    {
        _data = new Data();
        ReadFile();
    }

    public T Load<T>(string key)
    {
        Debug.Log($"Data Loaded: {_data.Collection}");

        if (_data.Collection.ContainsKey(key))
        {
            string dataString = string.Empty;
            _data.Collection.TryGetValue(key, out dataString);
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

        var datatType = typeof(T);
        if (datatType.IsPrimitive || datatType == typeof(string))
        {
            dataString = data.ToString();
        }
        else
        {
            dataString = JsonConvert.SerializeObject(data);
        }

        _data.Collection[key] = dataString;

        LogDictionary(_data.Collection);

        SaveFile(JsonConvert.SerializeObject(_data.Collection));
    }

    public void ReadFile(string filePath = "")
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = Path.Combine(Application.persistentDataPath, "data.json");
        }

        string json = File.ReadAllText(filePath);
        _data.Collection = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        Debug.Log($"Json: {json}");
    }

    private void SaveFile(string content, string filePath = "")
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = Path.Combine(Application.persistentDataPath, "data.json");
        }

        Debug.Log($"data path: {filePath}");
        File.WriteAllText(filePath, content);
    }

    private void LogDictionary(Dictionary<string, string> collection)
    {
        var log = new StringBuilder();
        foreach (var item in collection)
        {
            log.AppendLine($"{item.Key}: {item.Value}");
        }
        Debug.Log(log.ToString());
    }
}
