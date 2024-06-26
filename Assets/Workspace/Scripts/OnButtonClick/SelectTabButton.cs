using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTabButton : OnButtonClick
{
    public TabSelector TabSelector;
    public int Index;

    private void Start()
    {
        AddListener(() => TabSelector.SelectTab(Index));
    }
}
