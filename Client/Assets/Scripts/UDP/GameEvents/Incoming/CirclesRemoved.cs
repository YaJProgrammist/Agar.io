using System.Collections.Generic;
using UnityEngine;

public class CirclesRemoved : IncomingGameEvent
{
    private List<int> circlesId;
    private List<int> playersId;

    public CirclesRemoved(byte[] package)
    {
        if (package[0] != (byte)IncomingGameEventTypes.CirclesRemoved)
        {
            Debug.LogError("Incorrect package");
            return;
        }

        circlesId = new List<int>();

        int circlesCount = Deserializer.DeserializeInt(package, 1);

        int i = 5;

        for (int j = 0; j < circlesCount; j++)
        {
            circlesId.Add(Deserializer.DeserializeInt(package, i));
            i += 4;
        }

        while (i < package.Length)
        {
            playersId.Add(Deserializer.DeserializeInt(package, i));
            i += 4;
        }
    }

    public override void Handle()
    {
        PlayerManager.Instance.RemoveCircles(playersId, circlesId);
    }
}