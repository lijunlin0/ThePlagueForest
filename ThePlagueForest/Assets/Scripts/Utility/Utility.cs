using System;


public static class Utility
{
    public static void EnumForeach<T>(Callback<T> callback)
    {
        foreach(T value in Enum.GetValues(typeof(T)))
        {
            callback(value);
        }
    }
    public static int StrToInt(string str, int defaultValue = 0)
    {
        int value;
        bool valid = int.TryParse(str, out value);
        return valid ? value : defaultValue;
    }
    public static float StrToFloat(string str, float defaultValue = 0)
    {
        float value;
        bool valid = float.TryParse(str, out value);
        return valid ? value : defaultValue;
    }
}