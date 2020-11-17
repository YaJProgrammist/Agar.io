using System.Collections.Generic;

namespace Server.Events
{
    public class FoodAdded : OutgoingGameEvent
    {
        public List<Food> NewFood { get; set; }

        public FoodAdded(List<Food> food)
        {
            NewFood = food;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.FoodAdded);

            foreach (EatableObject eatableObject in NewFood)
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
