using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BulletShooter
{
    private Callback mShootCallback;
    private Callback mAnimationCallback;
    private Character mCharacter;
    private int mShootRange = 0;
    private float mShootTime;
    private float mDefaultTime=0;
    private float mAnimationTime=0;
    private float mDefaultAnimationTime=0;
    private bool mCanShoot=true;
    private EquipmentId mEquipmentId;

    
    public BulletShooter(EquipmentId equipmentId,Character character,Callback callback,float shootTime,int shootRange,float animationTime=0,Callback animationCallback=null)
    {
        mShootCallback = callback;
        mShootTime = shootTime;
        
        mAnimationCallback=animationCallback;
        mAnimationTime=animationTime;
        mShootRange=shootRange;
        mCharacter = character;
        mEquipmentId=equipmentId;
    }

    public void OnUpdate()
    {
        //如果有攻击动画
        if(mAnimationTime!=0)
        {
            HaveAnimationOnUpdate();
            return;
        }
        mCanShoot=HasTarget();
        //攻击
        if (mDefaultTime>=mShootTime&&mCanShoot)
        {
            mShootCallback();
            mDefaultTime=0;
            mDefaultAnimationTime=0;
        }
        float attackSpeedFactor=mCharacter.GetCurrentPropertySheet().GetAttackSpeedFactor();
        mDefaultTime+=Time.deltaTime*attackSpeedFactor;

    }

    private bool HasTarget()
    {
        Enemy nearEnemy=FightUtility.GetNearEnemy(mCharacter,mShootRange);
        return nearEnemy!=null;
    }
    public void HaveAnimationOnUpdate()
    {
        //判断能否攻击
        if(Vector3.Distance(mCharacter.transform.position,Player.GetCurrent().transform.position)>mShootRange)
        {
            mDefaultAnimationTime=0;
            mDefaultTime=0;
            return;
        }

        //播放攻击动画
        if (mDefaultAnimationTime>=mAnimationTime)
        {
            mAnimationCallback();
            mDefaultAnimationTime=0;
            DOVirtual.DelayedCall(mShootTime-mAnimationTime,()=>
            {
                mShootCallback();
                //重置计时
                mDefaultAnimationTime=0;
            });
        }
        mDefaultAnimationTime+=Time.deltaTime;
    }


    public EquipmentId GetEquipmentId(){return mEquipmentId;}
    public bool CanShoot(){return mCanShoot;}
}