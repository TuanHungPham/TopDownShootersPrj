using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class LogSystem
{
    public static void LogDictionary(Dictionary<string, string> collection, string dictionaryName)
    {
        if (collection == null)
        {
            Debug.LogWarning($"{dictionaryName} is null!");
            return;
        }

        var log = new StringBuilder();
        log.AppendLine($"{dictionaryName}: ");
        foreach (var item in collection)
        {
            log.AppendLine($"{item.Key}: {item.Value}");
        }
        Debug.Log(log.ToString());
    }
}
