using System.Collections.Generic;

namespace Server
{
    public class PlayerLeftEventArgs
    {
        public SortedSet<Player> ContainedPlayers { get; private set; }
        public SortedSet<Food> ContainedFood { get; private set; }
        public int PlayerId { get; private set; }

        public PlayerLeftEventArgs(SortedSet<Player> containedPlayers, SortedSet<Food> containedFood, int playerId)
        {
            ContainedPlayers = containedPlayers;
            ContainedFood = containedFood;
            PlayerId = playerId;
        }
    }
}
