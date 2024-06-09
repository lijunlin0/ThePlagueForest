using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public static class Utility
{
    public static void EnumForeach<T>(Callback<T> callback)
    {
        foreach(T value in Enum.GetValues(typeof(T)))
        {
            callback(value);
        }
    }
}