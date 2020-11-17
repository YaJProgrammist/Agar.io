using System.Collections.Generic;

namespace Server.Events
{
    public class EatableObjectRemoved : OutgoingGameEvent
    {
        public int EatenObjectId { get; set; }

        public EatableObjectRemoved(int eatenObjectId)
        {
            EatenObjectId = eatenObjectId;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.EatableObjectRemoved);
            serialized.AddRange(Serializer.SerializeInt(EatenObjectId));

            return serialized.ToArray();
        }
    }
}
