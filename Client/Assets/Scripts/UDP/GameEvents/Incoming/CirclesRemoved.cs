using System.Collections.Generic;
using UnityEngine;

public class CirclesRemoved : IncomingGameEvent
{
    private List<int> circlesId;

    public CirclesRemoved(byte[] package)
    {
        if (package[0] != (byte)IncomingGameEventTypes.CirclesRemoved)
        {
            Debug.LogError("Incorrect package");
            return;
        }

        circlesId = new List<int>();

        int i = 1;
        while (i < package.Length)
        {
            circlesId.Add(Deserializer.DeserializeInt(package, i));
            i += 4;
        }
    }

    public override void Handle()
    {

    }
}