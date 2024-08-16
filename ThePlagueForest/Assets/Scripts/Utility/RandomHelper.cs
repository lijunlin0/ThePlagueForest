using UnityEngine;

public class RandomHelper
{
    public static int RandomInt(int min=int.MinValue,int max=int.MaxValue)
    {
        return Random.Range(min,max);
    }
    public static float Randomfloat(float min,float max)
    {
        return Random.Range(min,max);
    }
    public static int RandomIntTwoRange(int min1,int max1,int min2,int max2)
    {
        bool range=Random.Range(0,2)==0;
        if(range)
        {
            return Random.Range(min1,max1);
        }
        else
        {
            return Random.Range(min2,max2);
        }
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