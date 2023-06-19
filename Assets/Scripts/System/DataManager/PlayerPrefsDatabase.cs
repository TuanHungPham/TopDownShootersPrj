using System;
using UnityEngine;

public class PlayerPrefsDatabase : IKeyValueDatabase
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
