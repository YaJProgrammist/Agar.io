using System;
using System.Collections.Generic;

namespace Server
{
    public static class Serializer
    {
        public static List<byte> SerializeInt(int initialValue)
        {
            List<byte> seriaized = new List<byte>{ 0, 0, 0, 0 };

            int i = 0;

            while(i < 4 && initialValue > 0)
            {
                seriaized[i] = (byte)(initialValue % 256);
                initialValue /= 256;
                i++;
            }

            return seriaized;
        }
        public static List<byte> SerializeDouble(double initialValue)
        {
            return SerializeInt((int)Math.Round(initialValue * 100));
        }
    }
}
