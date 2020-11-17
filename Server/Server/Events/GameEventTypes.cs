namespace Server.Events
{
    public enum GameEventTypes : byte
    {
        PositionsUpdated,
        EatableObjectRemoved,
        PlayerAdded,
        PlayerLeft,
        RoundStarted,
        RoundOver,
        CirclesAdded
    }
}
