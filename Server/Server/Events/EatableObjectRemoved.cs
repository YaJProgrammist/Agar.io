using System.Collections.Generic;

namespace Server.Events
{
    public class EatableObjectRemoved : GameEvent
    {
        public int EatenObjectId { get; set; }

        public EatableObjectRemoved(int eatenObjectId)
        {
            EatenObjectId = eatenObjectId;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)GameEventTypes.EatableObjectRemoved);
            serialized.AddRange(Serializer.SerializeInt(EatenObjectId));

            return serialized.ToArray();
        }
    }
}
