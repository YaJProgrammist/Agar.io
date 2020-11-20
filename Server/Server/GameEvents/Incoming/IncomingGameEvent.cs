namespace Server.Events
{
    public abstract class IncomingGameEvent
    {
        public abstract void Handle(Room room);
    }
}