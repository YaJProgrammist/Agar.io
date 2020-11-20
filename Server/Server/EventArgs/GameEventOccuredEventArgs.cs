using Server.Events;

namespace Server
{
    public class GameEventOccuredEventArgs
    {
        public OutgoingGameEvent GameEvent { get; private set; }
        public int? PlayerId { get; private set; }

        public GameEventOccuredEventArgs(OutgoingGameEvent gameEvent, int? playerId = null)
        {
            GameEvent = gameEvent;
            PlayerId = playerId;
        }
    }
}
