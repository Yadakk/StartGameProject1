using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLink : MonoBehaviour
{
    public TutorialLink Next;

    public void NextTutorial()
    {
        Debug.Log(gameObject.activeSelf);
        if (!gameObject.activeSelf) return;

        gameObject.SetActive(false);
        Next.gameObject.SetActive(true);
    }
}
