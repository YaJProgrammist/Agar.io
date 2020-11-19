using System.Collections.Generic;

namespace Server.Events
{
    public class CirclesFrameUpdate : OutgoingGameEvent
    {
        public List<Circle> Circles { get; set; }

        public CirclesFrameUpdate(List<Circle> circles)
        {
            Circles = circles;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.CirclesFrameUpdate);

            foreach (Circle circle in Circles)
            {
                serialized.AddRange(Serializer.SerializeInt(circle.Id));
                serialized.AddRange(Serializer.SerializeDouble(circle.Position.X));
                serialized.AddRange(Serializer.SerializeDouble(circle.Position.Y));
                serialized.AddRange(Serializer.SerializeDouble(circle.Radius));
            }

            return serialized.ToArray();
        }
    }
}
