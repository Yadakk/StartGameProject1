using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    public AppearingButton Transition;
    public PlayerValues PlayerValues;
    public GameFlower GameFlower;
    public PopupTutorial FirstDayPopup;
    public bool InjectData = true;

    public GameSave Data { get; private set; }

    private static readonly string _filePath = "GameSave";

    private void Start() => Load();

    public void Save()
    {
        if(!InjectData) return;
        Data = new(PlayerValues.Values.Money, GameFlower.CurrentDay);
        JsonSaver.Save(Data, _filePath);
    }

    public void Delete() => JsonSaver.Delete(_filePath);

    public void Load()
    {
        if (!JsonSaver.Load<GameSave>(_filePath, out var loadedObject))
        {
            if (InjectData) FirstDayEvents();
            return;
        }

        Data = loadedObject;
        if (!InjectData) return;
        PlayerValues.Values.Money = Data.Money;
        GameFlower.CurrentDay = Data.Day;
        Debug.Log(Data.Day);
        if (Data.Day > 1) GameFlower.StartDay();
        else
        {
            FirstDayEvents();
        }
    }

    private void FirstDayEvents()
    {
        FirstDayPopup.Appear();
        Transition.Image.color = Color.clear;
    }
}
