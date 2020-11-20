using UnityEngine;

public class PlayerAdded : IncomingGameEvent
{
    private int myPlayerId;

    public PlayerAdded(byte[] package)
    {
        if (package[0] != (byte)IncomingGameEventTypes.PlayerAdded)
        {
            Debug.LogError("Incorrect package");
            return;
        }

        myPlayerId = Deserializer.DeserializeInt(package, 1);
    }

    public override void Handle()
    {
        PlayerManager.Instance.currentPlayerId = myPlayerId;
    }
}
