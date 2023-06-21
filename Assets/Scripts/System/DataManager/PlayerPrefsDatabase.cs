using System;
using UnityEngine;

public class PlayerPrefsDatabase : IKeyValueDatabase
{
    public void SetInGameData<T>(string key, T data)
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
    }

    public T GetInGameData<T>(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string dataString = PlayerPrefs.GetString(key);
            var dataType = typeof(T);

            if (dataType.IsPrimitive || dataType == typeof(string))
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

    public void SaveToDatabase()
    {
    }

    public void LoadFromDatabase(string key = null)
    {
    }
}
