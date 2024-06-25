using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : OnButtonClick
{
    private void Start()
    {
        AddListener(() => Application.Quit());
    }
}
