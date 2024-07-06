using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseIfHasMoney : MonoBehaviour
{
    public PlayerValues PlayerValues;
    public OpenCloseWindow OpenCloseWindow;
    public GameOverChecker GameOverChecker;

    public void Close()
    {
        if (PlayerValues.Values.Money < 0) { GameOverChecker.Check(); return; }
        OpenCloseWindow.Close();
    }
}
