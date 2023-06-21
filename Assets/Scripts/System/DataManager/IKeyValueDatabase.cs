public interface IKeyValueDatabase
{
    /// <summary>
    /// Set data In-game to In-game Data Storage (Cache)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="data"></param>
    void SetInGameData<T>(string key, T data);
    /// <summary>
    /// Get data In-game in runtime from In-game Data Storage (Cache)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    T GetInGameData<T>(string key);
    /// <summary>
    /// Saving In-game data to Database
    /// Recomended: Use this fucntion when quitting application or saving important data.
    /// </summary>
    void SaveToDatabase();
    /// <summary>
    /// Loading data from database and synchornizing with In-game data
    /// </summary>
    /// <param name="key"></param>
    /// 
    void LoadFromDatabase(string key = null);
}

