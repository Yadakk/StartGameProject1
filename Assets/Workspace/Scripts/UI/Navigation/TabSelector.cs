using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabSelector : MonoBehaviour
{
    private int _selectedTab = -1;

    private void Start()
    {
        HideAll();
    }

    private void HideAll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SelectTab(int index)
    {
        if (index < 0) HideAll();
        else if (index == _selectedTab) ToggleTab();
        else SetTab(index);
    }

    private void SetTab(int index)
    {
        if (_selectedTab > -1) transform.GetChild(_selectedTab).gameObject.SetActive(false);
        transform.GetChild(index).gameObject.SetActive(true);
        _selectedTab = index;
    }

    private void ToggleTab()
    {
        var selectedGameObject = transform.GetChild(_selectedTab).gameObject;
        selectedGameObject.SetActive(!selectedGameObject.activeSelf);
    }
}
