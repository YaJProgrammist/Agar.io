namespace Server.Events
{
    public enum OutgoingGameEventTypes : byte
    {
        PositionsUpdated,
        CirclesRemoved,
        FoodRemoved,
        PlayerAdded,
        PlayerLeft,
        RoundStarted,
        RoundOver,
        CirclesAdded,
        FoodAdded,
        PlayerDied
    }
}
