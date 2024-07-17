using UnityEngine;

public class RandomHelper
{
    public static int RandomInt(int min=int.MinValue,int max=int.MaxValue)
    {
        return Random.Range(min,max);
    }

    public static bool RandomResult(float percent)
    {
        return RandomInt(0,100)<=percent;
    }
    public static bool PlayerRandomResultWithLucky(float percent)
    {
        double luckyFactor=Player.GetCurrent().GetCurrentPropertySheet().GetLuckyFactor();
        percent*=(float)luckyFactor;
        return RandomResult(percent);
    }
}