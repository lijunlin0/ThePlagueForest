using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter
{
    private Callback mShootCallback;
    private Callback mAnimationCallback;
    private float mShootTime;
    private float mDefaultTime=0;
    private float mAnimationTime=0;
    private float mDefaultAnimationTime=0;
    
    public BulletShooter(Callback callback,float shootTime,float animationTime=0,Callback animationCallback=null)
    {
        mShootCallback = callback;
        mShootTime = shootTime;
        
        mAnimationCallback=animationCallback;
        mAnimationTime=animationTime;
    }

    public void OnUpdate()
    {
        if (mDefaultTime>=mShootTime)
        {
            mShootCallback();
            mDefaultTime=0;
            mDefaultAnimationTime=0;
        }
        mDefaultTime+=Time.deltaTime;
        if(mAnimationTime==0)
        {
            return;
        }
        if (mDefaultAnimationTime>=mAnimationTime)
        {
            mAnimationCallback();
            mDefaultAnimationTime=0;
        }
        mDefaultAnimationTime+=Time.deltaTime;
    }
}