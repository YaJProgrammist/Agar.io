using System.Collections.Generic;

namespace Server.Events
{
    public class PlayerLeft : OutgoingGameEvent
    {
        public int PlayerId { get; set; }

        public PlayerLeft(int playerId)
        {
            PlayerId = playerId;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.PlayerLeft);
            serialized.AddRange(Serializer.SerializeInt(PlayerId));

            return serialized.ToArray();
        }
    }
}
