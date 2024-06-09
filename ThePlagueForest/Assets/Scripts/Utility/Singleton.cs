using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//单例基类
public class Singleton<T> where T : class, new()
{
    private static T sInstance=null;
    public Singleton(){}
    public static T GetInstance()
    {
        if(sInstance==null)
        {
            sInstance=new T();
            Type type=sInstance.GetType();
            MethodInfo method=type.GetMethod("OnSingletonInit");
            if(method!=null)
            {
                method.Invoke(sInstance,null);
            }
        }
        return sInstance;
    }
}
