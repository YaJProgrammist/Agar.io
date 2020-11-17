using System.Collections.Generic;

namespace Server.Events
{
    public class CirclesAdded : OutgoingGameEvent
    {
        public List<Circle> NewCircles { get; set; }

        public CirclesAdded(List<Circle> circles)
        {
            NewCircles = circles;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.CirclesAdded);

            foreach (Circle circle in NewCircles)
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
