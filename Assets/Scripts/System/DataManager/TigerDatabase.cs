using UnityEngine;
using TigerForge;
using Newtonsoft.Json;
using System;

public class TigerDatabase : IKeyValueDatabase
{
    #region public
    #endregion

    #region private
    private EasyFileSave newFile;
    #endregion

    public TigerDatabase()
    {
        newFile = new EasyFileSave();
    }

    public void SetInGameData<T>(string key, T data)
    {
        var dataType = typeof(T);

        if (dataType.IsPrimitive || dataType == typeof(string))
        {
            newFile.Add(key, data);
        }
        else
        {
            newFile.AddSerialized(key, data);
        }

        UserData.AddData(key, data);

        Debug.Log($"{key} : {data} Saving...");
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

    public void SaveToDatabase()
    {
        newFile.Append();
    }

    public void LoadFromDatabase(string key = null)
    {
        Debug.Log($"Key Loading.....: {key}");
        Debug.Log($"Loading Status: {newFile.Load()}");

        if (!newFile.KeyExists(key) || !newFile.Load()) return;

        Debug.Log($"Data Loaded: {key}");

        var data = newFile.GetData(key);
    }
}
