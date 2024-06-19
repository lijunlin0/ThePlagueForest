using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter
{
    private Callback mShootCallback;
    private float mShootTime;
    private float defaultTime=0;
    
    public BulletShooter(Callback callback,float shootTime)
    {
        mShootCallback = callback;
        mShootTime = shootTime;
        defaultTime=shootTime;
    }

    public void OnUpdate()
    {
        if (defaultTime>=mShootTime)
        {
            mShootCallback();
            defaultTime=0;
        }
        defaultTime+=Time.deltaTime;
    }
}