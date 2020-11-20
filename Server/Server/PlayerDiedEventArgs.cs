namespace Server
{
    public class PlayerDiedEventArgs
    {
        public Player DeadPlayer { get; private set; }

        public PlayerDiedEventArgs(Player deadPlayer)
        {
            DeadPlayer = deadPlayer;
        }
    }
}
