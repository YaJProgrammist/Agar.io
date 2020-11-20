using System.Collections.Generic;

namespace Server
{
    public class PlayerCirclesAddedEventArgs
    {
        public List<Circle> NewCircles { get; private set; }
        public int PlayerId { get; private set; }

        public PlayerCirclesAddedEventArgs(List<Circle> newCircles, int playerId)
        {
            NewCircles = newCircles;
            PlayerId = playerId;
        }
    }
}
