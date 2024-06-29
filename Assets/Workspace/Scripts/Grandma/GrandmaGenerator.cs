using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaGenerator : MonoBehaviour
{
    public GameObject Prefab;
    public Transform Destination;
    public Transform Exit;

    public GameObject Generate()
    {
        var grandma = Instantiate(Prefab, transform.position, transform.rotation, transform);
        var mover = grandma.GetComponent<GrandmaMoveToPosition>();
        mover.Generator = this;
        mover.Move(Destination);
        return grandma;
    }
}
