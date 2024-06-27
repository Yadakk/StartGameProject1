using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtensions
{
    public static List<T> ShuffleWithoutRepetition<T>(this IList<T> list)
    {
        if (list.Count <= 2) return list.ToList();
        T lastElem = list[^1];
        list = list.OrderBy(elem => Random.Range(0, list.Count)).ToList();
        int matchingLastCount = 0;
        while (EqualityComparer<T>.Default.Equals(list[matchingLastCount], lastElem))
        {
            matchingLastCount++;
            if (matchingLastCount >= list.Count) return list.ToList();
        }
        int servingIndex = 0;
        for (int i = matchingLastCount; i > 0; i--)
        {
            T servingElem = list[servingIndex];
            list.Remove(servingElem);
            list.Insert(Random.Range(matchingLastCount - servingIndex, list.Count), servingElem);
        }
        return list.ToList();
    }

    public static List<T> ShuffleWithoutRepetition<T>(this IList<T> list, T customLast)
    {
        list = list.OrderBy(elem => Random.Range(0, list.Count)).ToList();
        int matchingLastCount = 0;
        while (EqualityComparer<T>.Default.Equals(list[matchingLastCount], customLast))
        {
            matchingLastCount++;
            if (matchingLastCount >= list.Count) return list.ToList();
        }
        int servingIndex = 0;
        for (int i = matchingLastCount; i > 0; i--)
        {
            T servingElem = list[servingIndex];
            list.Remove(servingElem);
            list.Insert(Random.Range(matchingLastCount - servingIndex, list.Count), servingElem);
        }
        return list.ToList();
    }

    public static List<T> TakeRandom<T>(this IList<T> elements, int countToTake)
    {
        var selected = new List<T>();
        for (var i = 0; i < countToTake; ++i)
        {
            var next = Random.Range(0, elements.Count - selected.Count);
            selected.Add(elements[next]);
            elements[next] = elements[^selected.Count];
        }
        return selected;
    }
}