using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaGenerator : MonoBehaviour
{
    public GameObject Prefab;
    public Transform Destination;
    public Transform Exit;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        var grandma = Instantiate(Prefab, transform.position, transform.rotation, transform);
        var mover = grandma.GetComponent<MoveToPosition>();
        mover.Move(Destination);
    }
}
