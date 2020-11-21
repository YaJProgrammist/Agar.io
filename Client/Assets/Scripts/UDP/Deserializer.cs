using UnityEngine;

public static class Deserializer
{
    public static int DeserializeInt(byte[] initialArray, int startInd)
    {
        long deseriaized = 0;

        int i = 0;
        long factor = 1;

        while(i < 4 && startInd + i < initialArray.Length)
        {
            deseriaized += initialArray[startInd + i] * factor;

            i++;

            if (i >= 4)
            {
                break;
            }

            factor *= 256;
        }

        return (int)deseriaized;
    }
    public static double DeserializeDouble(byte[] initialArray, int startInd)
    {
        int deserializedInt = DeserializeInt(initialArray, startInd);
        return deserializedInt / 100.0;
    }
}
