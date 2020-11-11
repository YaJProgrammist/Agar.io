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

            serialized.Add((byte)GameEventTypes.RoundOver);

            foreach (Player player in PlayersPutOnField)
            {
                serialized.AddRange(Serializer.SerializeInt(player.Id));
                serialized.Add(0);

                Circle playerFirstCircle = player.playerCircles[0];
                serialized.AddRange(Serializer.SerializeInt(player.Id));
                serialized.Add(0);
                serialized.AddRange(Serializer.SerializeInt((int)Math.Round(playerFirstCircle.X)));
                serialized.Add(0);
                serialized.AddRange(Serializer.SerializeInt((int)Math.Round(playerFirstCircle.Y)));
                serialized.Add(0);
            }

            return serialized.ToArray();
        }
    }
}
