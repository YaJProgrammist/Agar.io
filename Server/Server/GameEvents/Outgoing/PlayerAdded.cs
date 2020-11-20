using System.Collections.Generic;

namespace Server.Events
{
    //When player has been connected to server, his id needs to be send to him
    public class PlayerAdded : OutgoingGameEvent
    {
        public int PlayerId { get; set; }

        public PlayerAdded(int playerId)
        {
            PlayerId = playerId;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.PlayerAdded);
            serialized.AddRange(Serializer.SerializeInt(PlayerId));

            return serialized.ToArray();
        }
    }
}
