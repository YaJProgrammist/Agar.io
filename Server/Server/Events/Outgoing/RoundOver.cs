using System.Collections.Generic;

namespace Server.Events
{
    public class RoundOver : OutgoingGameEvent
    {
        public List<Player> PlayersTop { get; set; }

        public RoundOver(List<Player> playersTop)
        {
            PlayersTop = playersTop;
        }

        public override byte[] GetSerialized()
        {
            List<byte> serialized = new List<byte>();

            serialized.Add((byte)OutgoingGameEventTypes.RoundOver);

            foreach (Player player in PlayersTop)
            {
                serialized.AddRange(Serializer.SerializeInt(player.Id));
                serialized.AddRange(Serializer.SerializeDouble(player.Score));
            }

            return serialized.ToArray();
        }
    }
}
