using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public List<GameSave> Saves = new();

    public GameData()
    {
        Saves.Add(new());
    }
}
