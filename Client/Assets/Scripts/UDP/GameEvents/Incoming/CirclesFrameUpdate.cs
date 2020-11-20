using System.Collections.Generic;
using UnityEngine;

public class CirclesFrameUpdate : IncomingGameEvent
{
    private List<int> playerId;
    private List<List<int>> circleId;
    private List<List<double>> circleX;
    private List<List<double>> circleY;
    private List<List<double>> circleRadius;

    public CirclesFrameUpdate(byte[] package)
    {
        if (package[0] != (byte)IncomingGameEventTypes.CirclesFrameUpdate)
        {
            Debug.LogError("Incorrect package");
            return;
        }

        playerId = new List<int>();
        circleId = new List<List<int>>();
        circleX = new List<List<double>>();
        circleY = new List<List<double>>();
        circleRadius = new List<List<double>>();

        int i = 1;
        int currentPlayerNum = 0;
        while (i < package.Length)
        {
            playerId.Add(Deserializer.DeserializeInt(package, i));
            i += 4;

            int circlesCount = Deserializer.DeserializeInt(package, i);
            i += 4;

            circleId[currentPlayerNum] = new List<int>();
            circleX[currentPlayerNum] = new List<double>();
            circleY[currentPlayerNum] = new List<double>();
            circleRadius[currentPlayerNum] = new List<double>();

            for (int j = 0; j < circlesCount; j++)
            {
                circleId[currentPlayerNum].Add(Deserializer.DeserializeInt(package, i));
                i += 4;

                circleX[currentPlayerNum].Add(Deserializer.DeserializeDouble(package, i));
                i += 4;

                circleY[currentPlayerNum].Add(Deserializer.DeserializeDouble(package, i));
                i += 4;

                circleRadius[currentPlayerNum].Add(Deserializer.DeserializeDouble(package, i));
                i += 4;
            }
        }
    }

    public override void Handle()
    {
            
    }
}
