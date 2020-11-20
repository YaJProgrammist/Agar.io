using System.Collections.Generic;

namespace Server.Events
{
    public class FoodRemoved : OutgoingGameEvent
    {
        public List<int> RemovedFoodId { get; set; }

        public FoodRemoved(List<int> removedFoodId)
        {
            RemovedFoodId = removedFoodId;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.FoodRemoved);

            foreach (int FoodId in RemovedFoodId)
            {
                serialized.AddRange(Serializer.SerializeInt(FoodId));
            }

            return serialized.ToArray();
        }
    }
}
