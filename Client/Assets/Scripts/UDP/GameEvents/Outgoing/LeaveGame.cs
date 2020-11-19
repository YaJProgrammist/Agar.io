using System.Collections.Generic;

public class LeaveGame : OutgoingGameEvent
{
    private int myPlayerId;

    public LeaveGame(int playerId)
    {
        myPlayerId = playerId;
    }

    public override byte[] GetSerialized()
    {
        List<byte> serialized = new List<byte>();

        serialized.Add((byte)OutgoingGameEventTypes.LeaveGame);

        serialized.AddRange(Serializer.SerializeInt(myPlayerId));

        return serialized.ToArray();
    }
}
