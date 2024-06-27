using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectedSaveSaver : MonoBehaviour
{
    public int SelectedSave;

    public SelectedSaveData Data { get; private set; }

    private static readonly string _fileName = "SelectedSave";

    private void OnDestroy() => Save();

    private void Start() => Load();

    public void Save()
    {
        Data = new(SelectedSave);
        JsonSaver.Save(Data, _fileName);
    }

    public void Load()
    {
        if (!JsonSaver.Load<SelectedSaveData>(_fileName, out var loadedObject)) return;
        Data = loadedObject;

        SelectedSave = Data.SelectedSave;
    }
}
