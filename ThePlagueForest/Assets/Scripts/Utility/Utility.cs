using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public static class Utility
{
    public static int WindowWidth=0;
    public static int WindowHeight=0;
    public static int WindowStartHeight=0;
    public static int ScreenWidth=0;
    public static int ScreenHeight=0;
    public  const int PCWindowHeight=1080;
    public  const int PCWindowStartHeight=2560;
    public  const int PhoneWindowHeight=1920;
    public const int PhoneWindowStartHeight=3840;
    public static bool IsPC=false;
    public static void InitScreen()
   {
      ScreenWidth=Screen.width;
      ScreenHeight=Screen.height;
      if(Screen.width<Screen.height)
      {
        WindowHeight=PhoneWindowHeight;
        WindowStartHeight=PhoneWindowStartHeight;
        IsPC=false;
      }
      else
      {
        WindowHeight=PCWindowHeight;
        WindowStartHeight=PCWindowStartHeight;
        IsPC=true;
      }
      WindowWidth=(int)((float)WindowHeight/ScreenHeight*ScreenWidth);
   }

    public static void CameraAnimation()
    {
        DOVirtual.Float(WindowStartHeight/2, WindowHeight/2, 2, value => Camera.main.orthographicSize = value).SetEase(Ease.InOutSine).OnComplete(()=>
        {
                FightManager.GetCurrent().CreateUI();
        });  // 动画效果
    }

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
    public static void ForceRebuildLayout(Transform transform)
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }
}