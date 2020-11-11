using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Events
{
    public abstract class GameEvent
    {
        public abstract byte[] GetSerialized();
    }
}