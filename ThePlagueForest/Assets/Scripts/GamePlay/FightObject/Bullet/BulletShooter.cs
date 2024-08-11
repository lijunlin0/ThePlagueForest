using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BulletShooter
{
    private Callback mShootCallback;
    private Callback mAnimationCallback;
    private Enemy mEnemy;
    private float mEnemyShootRange = 0;
    private float mShootTime;
    private float mDefaultTime=0;
    private float mAnimationTime=0;
    private float mDefaultAnimationTime=0;
    private bool mCanShoot=true;
    
    public BulletShooter(Callback callback,float shootTime,float animationTime=0,Callback animationCallback=null,Enemy enemy=null,float enemyShootRange=0)
    {
        mShootCallback = callback;
        mShootTime = shootTime;
        
        mAnimationCallback=animationCallback;
        mAnimationTime=animationTime;
        mEnemyShootRange=enemyShootRange;
        mEnemy=enemy;
    }

    public void OnUpdate()
    {
        //如果有攻击动画
        if(mAnimationTime!=0)
        {
            HaveAnimationOnUpdate();
            return;
        }
        //攻击
        if (mDefaultTime>=mShootTime&&mCanShoot)
        {
            mShootCallback();
            mDefaultTime=0;
            mDefaultAnimationTime=0;
        }
        mDefaultTime+=Time.deltaTime;

    }
    public void HaveAnimationOnUpdate()
    {
        //判断能否攻击
        if(Vector3.Distance(mEnemy.transform.position,Player.GetCurrent().transform.position)>mEnemyShootRange)
        {
            mCanShoot=false;
            mDefaultAnimationTime=0;
            mDefaultTime=0;
            return;
        }
        else
        {
            mCanShoot=true;
        }
        //播放攻击动画
        if (mDefaultAnimationTime>=mAnimationTime)
        {
            DOVirtual.DelayedCall(mShootTime-mAnimationTime,()=>
            {
                mShootCallback();
                //重置计时
                mDefaultAnimationTime=0;
            });
            mAnimationCallback();
            mDefaultAnimationTime=0;
        }
        mDefaultAnimationTime+=Time.deltaTime;
    }

    public void ReduceShootTime(float time)
    {
        mShootTime-=time;
    }
}