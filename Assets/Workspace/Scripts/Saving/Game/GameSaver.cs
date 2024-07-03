using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
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
        Data = new(PlayerValues.Values.MoneyForSaving, GameFlower.CurrentDay);
        JsonSaver.Save(Data, _filePath);
    }

    public void Delete() => JsonSaver.Delete(_filePath);

    public void Load()
    {
        if (!JsonSaver.Load<GameSave>(_filePath, out var loadedObject))
        {
            if (InjectData) FirstDayPopup.Appear(); 
            return;
        }

        Data = loadedObject;
        if (!InjectData) return;
        PlayerValues.Values.Money = Data.Money;
        GameFlower.CurrentDay = Data.Day;

        if (Data.Day > 1) GameFlower.StartDay();
        else FirstDayPopup.Appear();
    }
}
