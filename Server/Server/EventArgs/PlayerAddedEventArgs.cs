using System.Collections.Generic;

namespace Server
{
    public class PlayerAddedEventArgs
    {
        public List<Player> ContainedPlayers { get; private set; }
        public List<Food> ContainedFood { get; private set; }
        public int PlayerId { get; private set; }

        public PlayerAddedEventArgs(List<Player> containedPlayers, List<Food> containedFood, int playerId)
        {
            ContainedPlayers = containedPlayers;
            ContainedFood = containedFood;
            PlayerId = playerId;
        }
    }
}
