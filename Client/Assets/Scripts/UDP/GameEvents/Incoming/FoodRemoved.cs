using System.Collections.Generic;
using UnityEngine;

public class FoodRemoved : IncomingGameEvent
{
    private List<int> foodId;

    public FoodRemoved(byte[] package)
    {
        if (package[0] != (byte)IncomingGameEventTypes.FoodRemoved)
        {
            Debug.LogError("Incorrect package");
            return;
        }

        foodId = new List<int>();

        int i = 1;
        while (i < package.Length)
        {
            foodId.Add(Deserializer.DeserializeInt(package, i));
            i += 4;
        }
    }

    public override void Handle()
    {

    }
}
