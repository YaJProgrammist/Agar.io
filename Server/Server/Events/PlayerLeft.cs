using System.Collections.Generic;

namespace Server.Events
{
    public class PLayerLeft : GameEvent
    {
        public int PlayerId { get; set; }

        public PLayerLeft(int playerId)
        {
            PlayerId = playerId;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)GameEventTypes.PlayerLeft);
            serialized.AddRange(Serializer.SerializeInt(PlayerId));

            return serialized.ToArray();
        }
    }
}
