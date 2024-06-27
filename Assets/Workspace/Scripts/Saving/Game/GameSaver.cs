using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    public GameSave Data { get; private set; }

    private static readonly string _filePath = "GameSave";

    private void OnDestroy() => Save();

    private void Start() => Load();

    public void Save()
    {
        Data = new();
        JsonSaver.Save(Data, _filePath);
    }

    public void Delete() => JsonSaver.Delete(_filePath);

    public void Load()
    {
        if (!JsonSaver.Load<GameSave>(_filePath, out var loadedObject)) return;
        Data = loadedObject;
    }
}
