using System.Collections.Generic;

namespace Server.Events
{
    public class PlayerDied : OutgoingGameEvent
    {
        public int PlayerId { get; set; }

        public PlayerDied(int playerId)
        {
            PlayerId = playerId;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.PlayerDied);
            serialized.AddRange(Serializer.SerializeInt(PlayerId));

            return serialized.ToArray();
        }
    }
}
