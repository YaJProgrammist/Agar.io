using System.Collections.Generic;

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
        serialized.AddRange(Serializer.SerializeDouble(velocityX));
        serialized.AddRange(Serializer.SerializeDouble(velocityY));

        return serialized.ToArray();
    }
}
