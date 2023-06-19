using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class LogSystem
{
    public static void LogDictionary(Dictionary<string, string> collection)
    {
        var log = new StringBuilder();
        foreach (var item in collection)
        {
            log.AppendLine($"{item.Key}: {item.Value}");
        }
        Debug.Log(log.ToString());
    }
}
