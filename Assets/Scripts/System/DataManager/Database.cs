using System;
using System.Collections.Generic;
using UnityEngine;

public class Database : IKeyValueDatabase
{
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
            dataString = JsonUtility.ToJson(data);
        }

        PlayerPrefs.SetString(key, dataString);
        PlayerPrefs.Save();
    }

    public T Load<T>(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string dataString = PlayerPrefs.GetString(key);
            var datatType = typeof(T);

            if (datatType.IsPrimitive || datatType == typeof(string))
            {
                return (T)Convert.ChangeType(dataString, typeof(T));
            }
            else
            {
                return JsonUtility.FromJson<T>(dataString);
            }
        }
        else
        {
            return default(T);
        }
    }
}

[Serializable]
public class Data
{
    public Dictionary<string, string> Collection { get => collection; set => collection = value; }
    private Dictionary<string, string> collection;

    public Data()
    {
        Collection = new Dictionary<string, string>();
    }

    public Data(Dictionary<string, string> collection)
    {
        this.Collection = collection;
    }
}
