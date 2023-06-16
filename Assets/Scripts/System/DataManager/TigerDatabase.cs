using UnityEngine;
using TigerForge;

public class TigerDatabase : MonoBehaviour
{
    #region public
    #endregion

    #region private
    #endregion

    public void Save<T>(string key, T data)
    {
        EasyFileSave newFile = new EasyFileSave();

        newFile.Add(key, data);
        newFile.Save();
    }

    public T Load<T>(string key)
    {
        EasyFileSave newFile = new EasyFileSave();

        if (!newFile.KeyExists(key)) return default(T);

        string dataString = string.Empty;
        var datatType = typeof(T);

        if (datatType.IsPrimitive || datatType == typeof(string))
        {
        }
        else
        {
        }
    }
}
