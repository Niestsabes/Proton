using System.Collections.Generic;
using UnityEngine;

public class CustomRandom
{
    public static T RandomInList<T>(List<T> listItem)
    {
        int randIdx = Mathf.Min(listItem.Count - 1, Mathf.FloorToInt(Random.value * listItem.Count));
        return listItem[randIdx];
    }

    public static T RandomInArray<T>(T[] listItem)
    {
        int randIdx = Mathf.Min(listItem.Length - 1, Mathf.FloorToInt(Random.value * listItem.Length));
        return listItem[randIdx];
    }
}