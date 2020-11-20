using System.Collections.Generic;

namespace Server.Events
{
    public class CirclesRemoved : OutgoingGameEvent
    {
        public List<int> RemovedCirclesId { get; set; }
        public List<int> PlayersId { get; set; }

        public CirclesRemoved(List<int> removedCirclesId, List<int> players)
        {
            RemovedCirclesId = removedCirclesId;
            PlayersId = players;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.CirclesRemoved);

            serialized.AddRange(Serializer.SerializeInt(RemovedCirclesId.Count));

            foreach (int circleId in RemovedCirclesId)
            {
                serialized.AddRange(Serializer.SerializeInt(circleId));
            }

            foreach (int playerId in PlayersId)
            {
                serialized.AddRange(Serializer.SerializeInt(playerId));
            }

            return serialized.ToArray();
        }
    }
}
