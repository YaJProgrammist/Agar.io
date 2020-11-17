namespace Server.Events
{
    public enum OutgoingGameEventTypes : byte
    {
        PositionsUpdated,
        EatableObjectRemoved,
        PlayerAdded,
        PlayerLeft,
        RoundStarted,
        RoundOver,
        CirclesAdded,
        FoodAdded,
        PlayerDied
    }
}
