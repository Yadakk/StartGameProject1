using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaGenerator : MonoBehaviour
{
    public SkipButton SkipButton;
    public GameObject Prefab;
    public GameObject RentmanPrefab;
    public Transform Destination;
    public Transform Exit;
    public List<GrandmaData> GrandmaDatas;

    private List<GrandmaData> _grandmasRemaining = new();

    public GameObject Generate()
    {
        var grandma = Instantiate(Prefab, transform.position, transform.rotation, transform);
        var mover = grandma.GetComponent<GrandmaMoveToPosition>();
        var holder = grandma.GetComponent<GrandmaDataHolder>();
        mover.Generator = this;

        if (_grandmasRemaining.Count == 0) _grandmasRemaining = GrandmaDatas.ShuffleWithoutRepetition();
        var selectedGrandma = _grandmasRemaining[Random.Range(0, _grandmasRemaining.Count)];
        holder.GrandmaData = selectedGrandma;
        _grandmasRemaining.Remove(selectedGrandma);
        holder.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0f, holder.GrandmaData.DownOffset);

        mover.SetSprites();
        mover.Move(Destination);
        return grandma;
    }

    public GameObject GenerateRentman()
    {
        var grandma = Instantiate(RentmanPrefab, transform.position, transform.rotation, transform);
        var mover = grandma.GetComponent<RentmanMover>();
        var holder = grandma.GetComponent<GrandmaDataHolder>();
        mover.Generator = this;
        mover.SkipButton = SkipButton;

        holder.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0f, holder.GrandmaData.DownOffset);

        mover.SetSprites();
        mover.Move(Destination);
        return grandma;
    }
}
