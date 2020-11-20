using UnityEngine;

public class PlayerDied : IncomingGameEvent
{
    private int playerId;

    public PlayerDied(byte[] package)
    {
        if (package[0] != (byte)IncomingGameEventTypes.PlayerDied)
        {
            Debug.LogError("Incorrect package");
            return;
        }

        playerId = Deserializer.DeserializeInt(package, 1);
    }

    public override void Handle()
    {
        //to do rewrite
        if (playerId == PlayerManager.Instance.currentPlayerId)
            GameManager.Instance.CurrentPlayerLeft();
    }
}
