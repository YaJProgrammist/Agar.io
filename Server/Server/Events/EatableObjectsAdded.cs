using System.Collections.Generic;

namespace Server.Events
{
    public class EatableObjectsAdded : GameEvent
    {
        public List<EatableObject> NewEatableObjects { get; set; }

        public EatableObjectsAdded(List<EatableObject> eatableObjects)
        {
            NewEatableObjects = eatableObjects;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)GameEventTypes.EatableObjectRemoved);

            foreach (EatableObject eatableObject in NewEatableObjects)
            {
                serialized.AddRange(Serializer.SerializeInt(eatableObject.Id));
                serialized.AddRange(Serializer.SerializeDouble(eatableObject.X));
                serialized.AddRange(Serializer.SerializeDouble(eatableObject.Y));
                serialized.AddRange(Serializer.SerializeDouble(eatableObject.Radius));
            }

            return serialized.ToArray();
        }
    }
}
