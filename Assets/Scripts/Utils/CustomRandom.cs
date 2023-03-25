using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomRandom
{
    public static int RandomInt(int max)
    {
        return Mathf.Min(Mathf.FloorToInt(Random.value * max), max - 1);
    }

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

    public static List<T> RandomizeList<T>(List<T> listItem)
    {
        List<T> randItem = new List<T>(listItem);
        for (int i = 0; i < randItem.Count; i++) {
            int rIdx = CustomRandom.RandomInt(randItem.Count);
            T value = randItem[rIdx];
            randItem[rIdx] = randItem[i];
            randItem[i] = value;
        }
        return randItem;
    }
}