using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : OnButtonClick
{
    public Pauser Pauser;

    private void Start() => AddListener(Pauser.TogglePause);
}
