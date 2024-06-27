using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour
{
    public GameObject PauseUI;

    private bool _paused;
    public bool Paused
    {
        get => _paused;
        set
        {
            _paused = value;
            Time.timeScale = _paused ? 1f : 0f;
            PauseUI.SetActive(_paused);
        }
    }

    public void TogglePause() => Paused = !Paused;
}