using System;
using System.Collections.Generic;

namespace Server.Events
{
    public class RoundStarted : GameEvent
    {
        public List<Player> PlayersPutOnField { get; set; }

        public RoundStarted(List<Player> players)
        {
            PlayersPutOnField = players;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)GameEventTypes.RoundStarted);

            foreach (Player player in PlayersPutOnField)
            {
                serialized.AddRange(Serializer.SerializeInt(player.Id));

                Circle playerFirstCircle = player.PlayerCircles[0];
                serialized.AddRange(Serializer.SerializeDouble(playerFirstCircle.Position.X));
                serialized.AddRange(Serializer.SerializeDouble(playerFirstCircle.Position.Y));
            }

            return serialized.ToArray();
        }
    }
}
