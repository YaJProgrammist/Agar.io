using System.Collections.Generic;

namespace Server.Events
{
    public class PlayerAdded : OutgoingGameEvent
    {
        public int PlayerId { get; set; }
        public int FirstCircleX { get; set; }
        public int FirstCircleY { get; set; }

        public PlayerAdded(int playerId, int firstCircleX, int firstCircleY)
        {
            PlayerId = playerId;
            FirstCircleX = firstCircleX;
            FirstCircleY = firstCircleY;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.PlayerAdded);
            serialized.AddRange(Serializer.SerializeInt(PlayerId));
            serialized.AddRange(Serializer.SerializeInt(FirstCircleX));
            serialized.AddRange(Serializer.SerializeInt(FirstCircleY));

            return serialized.ToArray();
        }
    }
}
