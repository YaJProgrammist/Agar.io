using System.Collections.Generic;

public class Split : OutgoingGameEvent
{
    private int myPlayerId;

    public Split(int playerId)
    {
        myPlayerId = playerId;
    }

    public override byte[] GetSerialized()
    {
        List<byte> serialized = new List<byte>();

        serialized.Add((byte)OutgoingGameEventTypes.Split);

        serialized.AddRange(Serializer.SerializeInt(myPlayerId));

        return serialized.ToArray();
    }
}
