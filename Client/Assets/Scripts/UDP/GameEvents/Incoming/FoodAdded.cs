using System.Collections.Generic;
using UnityEngine;

public class FoodAdded : IncomingGameEvent
{
    private List<int> foodId; 
    private List<double> foodX;
    private List<double> foodY;
    private List<double> foodRadius;

    public FoodAdded(byte[] package)
    {
        if (package[0] != (byte)IncomingGameEventTypes.FoodAdded)
        {
            Debug.LogError("Incorrect package");
            return;
        }

        foodId = new List<int>();
        foodX = new List<double>();
        foodY = new List<double>();
        foodRadius = new List<double>();

        int i = 1;
        while (i < package.Length)
        {
            foodId.Add(Deserializer.DeserializeInt(package, i));
            i += 4;

            foodX.Add(Deserializer.DeserializeDouble(package, i));
            i += 4;

            foodY.Add(Deserializer.DeserializeDouble(package, i));
            i += 4;

            foodRadius.Add(Deserializer.DeserializeDouble(package, i));
            i += 4;
        }
    }

    public override void Handle()
    {
            
    }
}
