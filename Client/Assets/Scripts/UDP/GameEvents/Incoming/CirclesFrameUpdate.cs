using System.Collections.Generic;
using UnityEngine;

public class CirclesFrameUpdate : IncomingGameEvent
{
    private List<int> circleId;
    private List<double> circleX;
    private List<double> circleY;
    private List<double> circleRadius;

    public CirclesFrameUpdate(byte[] package)
    {
        if (package[0] != (byte)IncomingGameEventTypes.CirclesFrameUpdate)
        {
            Debug.LogError("Incorrect package");
            return;
        }

        circleId = new List<int>();
        circleX = new List<double>();
        circleY = new List<double>();
        circleRadius = new List<double>();

        int i = 1;
        while (i < package.Length)
        {
            circleId.Add(Deserializer.DeserializeInt(package, i));
            i += 4;

            circleX.Add(Deserializer.DeserializeDouble(package, i));
            i += 4;

            circleY.Add(Deserializer.DeserializeDouble(package, i));
            i += 4;

            circleRadius.Add(Deserializer.DeserializeDouble(package, i));
            i += 4;
        }
    }

    public override void Handle()
    {
            
    }
}
