namespace Server.Events
{
    public class OnPackageIncameEventArgs
    {
        public IncomingGameEvent GameEvent { get; private set; }

        public OnPackageIncameEventArgs(IncomingGameEvent gameEvent)
        {
            GameEvent = gameEvent;
        }
    }
}
