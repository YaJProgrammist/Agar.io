using UnityEngine;

public class PlayerLeft : IncomingGameEvent
{
    private int playerId;

    public PlayerLeft(byte[] package)
    {
        if (package[0] != (byte)IncomingGameEventTypes.PlayerLeft)
        {
            Debug.LogError("Incorrect package");
            return;
        }

        playerId = Deserializer.DeserializeInt(package, 0);
    }

    public override void Handle()
    {
        
    }
}
