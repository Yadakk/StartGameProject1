using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnStart : MonoBehaviour
{
    public bool IsActive;

    private void Start() => gameObject.SetActive(IsActive);
}
