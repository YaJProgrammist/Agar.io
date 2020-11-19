using System.Collections.Generic;

namespace Server.Events
{
    public class CirclesRemoved : OutgoingGameEvent
    {
        public List<int> RemovedCirclesId { get; set; }

        public CirclesRemoved(List<int> removedCirclesId)
        {
            RemovedCirclesId = removedCirclesId;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.CirclesRemoved);

            foreach (int circleId in RemovedCirclesId)
            {
                serialized.AddRange(Serializer.SerializeInt(circleId));
            }

            return serialized.ToArray();
        }
    }
}
