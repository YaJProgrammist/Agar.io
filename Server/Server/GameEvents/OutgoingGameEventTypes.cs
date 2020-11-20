namespace Server.Events
{
    public enum OutgoingGameEventTypes : byte
    {
        CirclesFrameUpdate,
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
