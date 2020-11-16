using System.Collections.Generic;

namespace Server.Events
{
    public class CirclesAdded : GameEvent
    {
        public List<Circle> NewCircles { get; set; }

        public CirclesAdded(List<Circle> circles)
        {
            NewCircles = circles;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)GameEventTypes.RoundOver);

            foreach (Circle circle in NewCircles)
            {
                serialized.AddRange(Serializer.SerializeInt(circle.Id));
                serialized.AddRange(Serializer.SerializeDouble(circle.X));
                serialized.AddRange(Serializer.SerializeDouble(circle.Y));
            }

            return serialized.ToArray();
        }
    }
}
