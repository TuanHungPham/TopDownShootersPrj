public interface IKeyValueDatabase
{
    void Save<T>(string key, T data);
    T Load<T>(string key);
}

