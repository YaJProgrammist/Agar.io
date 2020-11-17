using System.Collections.Generic;

namespace Server.Events
{
    public class PositionsUpdated : OutgoingGameEvent
    {
        public List<Player> Players { get; set; }

        public PositionsUpdated(List<Player> players)
        {
            Players = players;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.PositionsUpdated);

            foreach (Player player in Players)
            {
                serialized.AddRange(Serializer.SerializeInt(player.Id));
                serialized.AddRange(Serializer.SerializeInt(player.PlayerCircles.Count));

                foreach (Circle circle in player.PlayerCircles)
                {
                    serialized.AddRange(Serializer.SerializeDouble(circle.Position.X));
                    serialized.AddRange(Serializer.SerializeDouble(circle.Position.Y));
                }
            }

            return serialized.ToArray();
        }
    }
}
