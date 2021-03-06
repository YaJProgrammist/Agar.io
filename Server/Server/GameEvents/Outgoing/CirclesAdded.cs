﻿using System.Collections.Generic;

namespace Server.Events
{
    public class CirclesAdded : OutgoingGameEvent
    {
        public List<Circle> NewCircles { get; set; }
        public int PlayerId { get; set; }

        public CirclesAdded(List<Circle> circles, int playerId)
        {
            NewCircles = circles;
            PlayerId = playerId;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.CirclesAdded);

            serialized.AddRange(Serializer.SerializeInt(PlayerId));

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
