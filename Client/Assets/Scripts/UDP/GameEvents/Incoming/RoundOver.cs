using System.Collections.Generic;
using UnityEngine;

public class RoundOver : IncomingGameEvent
{
    private List<int> playersId;
    private List<double> playerScore;

    public RoundOver(byte[] package)
    {
        if (package[0] != (byte)IncomingGameEventTypes.RoundOver)
        {
            Debug.LogError("Incorrect package");
            return;
        }

        playersId = new List<int>();
        playerScore = new List<double>();

        int i = 0;
        while (i < package.Length)
        {
            playersId.Add(Deserializer.DeserializeInt(package, i));
            i += 4;

            playerScore.Add(Deserializer.DeserializeDouble(package, i));
            i += 4;
        }
    }

    public override void Handle()
    {
            
    }
}
