using UnityEngine;

public class RandomPicker
{
    
    public static int PickRandom(int minValue, int maxValue)
    {
       
        return Random.Range(minValue, maxValue + 1);
    }
}

