using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : FightObject
{    
    //子弹移动速度
    protected int mBulletMoveSpeed=900;
    protected double mLiveTime;
    protected double mMaxLifeTime;
    protected override void Init()
    {
        base.Init();
        mLiveTime=0;
        mMaxLifeTime=5;
    }
    public virtual void OnUpdate()
    {
        mLiveTime+=Time.deltaTime;
        if(mLiveTime>=mMaxLifeTime)
        {
            mIsDead=true;
        }
    }

}
