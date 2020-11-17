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

            foreach (EatableObject eatableObject in NewCircles)
            {
                serialized.AddRange(Serializer.SerializeInt(eatableObject.Id));
                serialized.AddRange(Serializer.SerializeDouble(eatableObject.Position.X));
                serialized.AddRange(Serializer.SerializeDouble(eatableObject.Position.Y));
                serialized.AddRange(Serializer.SerializeDouble(eatableObject.Radius));
            }

            return serialized.ToArray();
        }
    }
}
