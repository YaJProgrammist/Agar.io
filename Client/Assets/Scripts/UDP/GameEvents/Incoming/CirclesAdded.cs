using System.Collections.Generic;
using UnityEngine;

public class CirclesAdded : IncomingGameEvent
{
    private List<int> circlesId; 
    private List<double> circleX;
    private List<double> circleY;
    private List<double> circleRadius;
    private int playerId;

    public CirclesAdded(byte[] package)
    {
        if (package[0] != (byte)IncomingGameEventTypes.CirclesAdded)
        {
            Debug.LogError("Incorrect package");
            return;
        }

        circlesId = new List<int>();
        circleX = new List<double>();
        circleY = new List<double>();
        circleRadius = new List<double>();

        playerId = Deserializer.DeserializeInt(package, 1);

        int i = 5;
        while (i < package.Length)
        {
            circlesId.Add(Deserializer.DeserializeInt(package, i));
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
        PlayerManager.Instance.AddCircles(playerId, circlesId, circleX, circleY, circleRadius); 
    }
}
