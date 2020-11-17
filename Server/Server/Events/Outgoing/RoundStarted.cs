using System;
using System.Collections.Generic;

namespace Server.Events
{
    public class RoundStarted : OutgoingGameEvent
    {
        public List<Player> PlayersPutOnField { get; set; }

        public RoundStarted(List<Player> players)
        {
            PlayersPutOnField = players;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.RoundStarted);

            foreach (Player player in PlayersPutOnField)
            {
                serialized.AddRange(Serializer.SerializeInt(player.Id));

                Circle playerFirstCircle = player.PlayerCircles[0];
                serialized.AddRange(Serializer.SerializeInt(playerFirstCircle.Id));
                serialized.AddRange(Serializer.SerializeDouble(playerFirstCircle.Position.X));
                serialized.AddRange(Serializer.SerializeDouble(playerFirstCircle.Position.Y));
            }

            return serialized.ToArray();
        }
    }
}
