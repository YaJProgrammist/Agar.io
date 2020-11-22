using System.Collections.Generic;

namespace Server
{
    public class PlayerAddedEventArgs
    {
        public SortedSet<Player> ContainedPlayers { get; private set; }
        public SortedSet<Food> ContainedFood { get; private set; }
        public int PlayerId { get; private set; }

        public PlayerAddedEventArgs(SortedSet<Player> containedPlayers, SortedSet<Food> containedFood, int playerId)
        {
            ContainedPlayers = containedPlayers;
            ContainedFood = containedFood;
            PlayerId = playerId;
        }
    }
}
