using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class OnButtonClick : MonoBehaviour
{
    private Button _button;
    public Button Button
    {
        get
        {
            if (_button == null) _button = GetComponent<Button>();
            return _button;
        }
    }

    public void AddListener(UnityAction action)
    {
        Button.onClick.AddListener(action);
    }
}
