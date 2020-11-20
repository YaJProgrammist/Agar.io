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

            foreach (Food food in NewFood)
            {
                serialized.AddRange(Serializer.SerializeInt(food.Id));
                serialized.AddRange(Serializer.SerializeDouble(food.Position.X));
                serialized.AddRange(Serializer.SerializeDouble(food.Position.Y));
                serialized.AddRange(Serializer.SerializeDouble(food.Radius));
            }

            return serialized.ToArray();
        }
    }
}
