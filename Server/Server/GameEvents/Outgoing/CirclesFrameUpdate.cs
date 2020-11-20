using System.Collections.Generic;

namespace Server.Events
{
    public class CirclesFrameUpdate : OutgoingGameEvent
    {
        public List<Player> Players { get; set; }

        public CirclesFrameUpdate(List<Player> players)
        {
            Players = players;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.CirclesFrameUpdate);

            foreach (Player player in Players)
            {
                serialized.AddRange(Serializer.SerializeInt(player.Id));
                serialized.AddRange(Serializer.SerializeInt(player.PlayerCircles.Count));

                foreach (Circle circle in player.PlayerCircles)
                {
                    serialized.AddRange(Serializer.SerializeInt(circle.Id));
                    serialized.AddRange(Serializer.SerializeDouble(circle.Position.X));
                    serialized.AddRange(Serializer.SerializeDouble(circle.Position.Y));
                    serialized.AddRange(Serializer.SerializeDouble(circle.Radius));
                }
            }

            return serialized.ToArray();
        }
    }
}
