using System.Collections.Generic;
using UnityEngine;

public class ChangeVelocity : OutgoingGameEvent
{
    private int myPlayerId;
    private double velocityX;
    private double velocityY;

    public ChangeVelocity(int playerId, double x, double y)
    {
        myPlayerId = playerId;
        velocityX = x;
        velocityY = y;
    }

    public override byte[] GetSerialized()
    {
        List<byte> serialized = new List<byte>();

        serialized.Add((byte)OutgoingGameEventTypes.ChangeVelocity);

        serialized.AddRange(Serializer.SerializeInt(myPlayerId));
        serialized.AddRange(Serializer.SerializeNormalizedVectorCoord(velocityX));
        serialized.AddRange(Serializer.SerializeNormalizedVectorCoord(velocityY));

        return serialized.ToArray();
    }
}
