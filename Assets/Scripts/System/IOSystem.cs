using System.IO;
using UnityEngine;

public static class IOSystem
{
    public static void LoadCharacterData(string fileName, LoadedCharacterData data)
    {
        string json = ReadFromFIle(fileName);
        if (json == null) return;

        JsonUtility.FromJsonOverwrite(json, data);
    }

    public static void SaveCharacterData(string fileName, CharacterData data)
    {
        string json = JsonUtility.ToJson(data);
        WriteToFile(fileName, json);
    }

    public static void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);

        CreateNewFolder(Application.dataPath + "/Saves");

        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    public static string ReadFromFIle(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }

        Debug.LogWarning("File not found");
        return null;
    }

    public static void CreateNewFolder(string path)
    {
        if (Directory.Exists(path)) return;

        Directory.CreateDirectory(path);

    }

    public static string GetFilePath(string fileName)
    {
        return Application.dataPath + "/Saves/" + fileName;
    }
}
