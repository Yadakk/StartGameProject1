using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaGenerator : MonoBehaviour
{
    public GameObject Prefab;
    public Transform Destination;
    public Transform Exit;
    public List<GrandmaData> GrandmaDatas;

    public GameObject Generate()
    {
        var grandma = Instantiate(Prefab, transform.position, transform.rotation, transform);
        var mover = grandma.GetComponent<GrandmaMoveToPosition>();
        var holder = grandma.GetComponent<GrandmaDataHolder>();
        mover.Generator = this;
        mover.Move(Destination);
        holder.GrandmaData = GrandmaDatas[Random.Range(0, GrandmaDatas.Count)];
        mover.SetSprites();
        return grandma;
    }
}
