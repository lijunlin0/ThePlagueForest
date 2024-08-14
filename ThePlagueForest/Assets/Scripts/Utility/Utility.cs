using System;


public static class Utility
{
    public  const int PcWidth=1920;
    public const int PcHeight=1080;
    public const int PhoneWidth=1080;
    public const int PhoneHeight=1920;
    public static int WindowWidth=0;
    public static int WindowHeight=0;
    public static bool IsPC=false;
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