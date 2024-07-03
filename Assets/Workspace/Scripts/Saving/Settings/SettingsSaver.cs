using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettingsSaver : MonoBehaviour
{
    public List<SliderSetting> SliderSettings;

    public SettingsData Data { get; private set; }

    private static readonly string _fileName = "Settings";

    private void OnDestroy() => Save();

    private void Start() => Load();

    public void Save()
    {
        Data = new(SliderSettings.Select(setting => setting.Slider.value).ToList());
        JsonSaver.Save(Data, _fileName);
    }

    public void Load()
    {
        if (!JsonSaver.Load<SettingsData>(_fileName, out var loadedObject)) return;
        Data = loadedObject;
        for (int i = 0; i < SliderSettings.Count; i++)
        {
            SliderSettings[i].Slider.value = Data.SliderValues[i];
        }
    }
}
