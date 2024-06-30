using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleSetter : MonoBehaviour
{
    public float TimeScale = 1f;

    private void Start()
    {
        Time.timeScale = TimeScale;
    }
}
