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

    public void Save<T>(string key, T data)
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

        newFile.Append();

        Debug.Log($"{key} : {data} Data Saving...");
    }

    public T Load<T>(string key)
    {
        Debug.Log($"Key Loading.....: {key}");
        Debug.Log($"Loading Status: {newFile.Load()}");

        if (!newFile.KeyExists(key) || !newFile.Load()) return default(T);

        Debug.Log($"Data Loaded: {key}");

        var dataType = typeof(T);

        if (dataType.IsPrimitive || dataType == typeof(string))
        {
            return (T)newFile.GetData(key);
        }
        else
        {
            return (T)newFile.GetDeserialized(key, dataType);
        }
    }
}
