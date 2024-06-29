using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlower : MonoBehaviour
{
    public GrandmaGenerator Generator;

    public int GrandmasPerDay = 6;

    private int _grandmasToGo;

    private void Start()
    {
        StartDay();
    }

    private void StartDay()
    {
        _grandmasToGo = GrandmasPerDay;
        GenerateGrandma();
    }

    private void GenerateGrandma()
    {
        if (_grandmasToGo == 0) { EndDay(); return; }
        var grandma = Generator.Generate();
        _grandmasToGo--;
        grandma.GetComponent<GrandmaMoveToPosition>().OnGrandmaExit.AddListener(GenerateGrandma);
    }

    private void EndDay()
    {
        Debug.Log("Day End");
    }
}
