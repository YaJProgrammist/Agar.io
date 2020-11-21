using System.Collections.Generic;

namespace Server
{
    public class PlayerCirclesRemovedEventArgs
    {
        public List<Circle> NewCircles { get; private set; }
        public int PlayerId { get; private set; }

        public PlayerCirclesRemovedEventArgs(List<Circle> newCircles, int playerId)
        {
            NewCircles = newCircles;
            PlayerId = playerId;
        }
    }
}
