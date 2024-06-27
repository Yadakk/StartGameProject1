using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEngine.JsonUtility;

public static class JsonSaver
{
    public static readonly string _savingPath = "Json";

    public static void Save<T>(T obj, string filePath)
    {
        Directory.CreateDirectory(Path.Combine(new string[] { Application.persistentDataPath, _savingPath }));
        var path = Path.Combine(new string[] { Application.persistentDataPath, _savingPath, filePath + ".json"});
        var json = ToJson(obj);
        File.WriteAllText(path, json);
    }

    public static bool Load<T>(string filePath, out T loadedObject)
    {
        var path = Path.Combine(new string[] { Application.persistentDataPath, _savingPath, filePath + ".json" });
        loadedObject = default;
        if (!File.Exists(path)) return false;
        var json = File.ReadAllText(path);
        loadedObject = FromJson<T>(json);
        return true;
    }

    public static void Delete(string filePath)
    {
        var path = Path.Combine(new string[] { Application.persistentDataPath, _savingPath, filePath + ".json" });
        if (!File.Exists(path)) return;
        File.Delete(path);
    }

    public static void ClearFolder()
    {
        DirectoryInfo dirToClear = new(Path.Combine(new string[] { Application.persistentDataPath, _savingPath }));

        foreach (FileInfo file in dirToClear.GetFiles())
        {
            file.Delete();
        }

        foreach (DirectoryInfo dir in dirToClear.GetDirectories())
        {
            dir.Delete(true);
        }
    }
}