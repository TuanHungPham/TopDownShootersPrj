using UnityEngine;
using TigerForge;

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
        newFile.Append();

        Debug.Log($"{key} : {data} Saving...");
    }

    public T GetInGameData<T>(string key)
    {
        Debug.Log($"Key Loading.....: {key}");
        Debug.Log($"Loading Status: {newFile.Load()}");

        if (!newFile.KeyExists(key) || !newFile.Load()) return default(T);

        Debug.Log($"Data Loaded: {key}");

        var dataType = typeof(T);
        object data;

        if (dataType.IsPrimitive || dataType == typeof(string))
        {
            data = newFile.GetData(key);
            // return (T)newFile.GetData(key);
        }
        else
        {
            data = newFile.GetDeserialized(key, dataType);
            // return (T)newFile.GetDeserialized(key, dataType);
        }
        UserData.AddData("key", data);

        return (T)data;
    }

    public void SaveToDatabase()
    {
    }

    public void LoadFromDatabase(string key = null)
    {
    }
}
