namespace Server.Events
{
    public abstract class OutgoingGameEvent
    {
        public abstract byte[] GetSerialized();
    }
}