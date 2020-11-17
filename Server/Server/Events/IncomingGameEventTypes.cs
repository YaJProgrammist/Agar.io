namespace Server.Events
{
    public enum IncomingGameEventTypes : byte
    {
        ChangeVelocity,
        Split,
        ConnectionToServer,
        LeaveGame
    }
}
