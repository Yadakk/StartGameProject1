using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTutorial : MonoBehaviour
{
    public PopupTutorial FlagDependence;

    private bool _flag;
    public bool Flag { get => _flag; private set => _flag = value; }
    public float DisappearTime;

    public void Appear()
    {
        if (Flag) return;
        if (FlagDependence != null)
            if (!FlagDependence.Flag) return;

        gameObject.SetActive(true);
        if (DisappearTime > 0) StartCoroutine(DisappearCoroutine());
        Flag = true;
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator DisappearCoroutine()
    {
        yield return new WaitForSeconds(DisappearTime);
        Disappear();
    }
}
