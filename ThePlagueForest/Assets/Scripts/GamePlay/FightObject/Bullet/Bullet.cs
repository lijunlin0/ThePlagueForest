using System.Collections.Generic;
using UnityEngine;

public class Bullet : FightObject
{    
    //子弹移动速度
    protected float mMoveSpeed=900;
    protected double mLiveTime;
    protected double mMaxLifeTime;
    //子弹来源角色
    protected Character mSource;
    //子弹伤害
    protected int mPoints=1;
    protected List<Character> mIgnoreList;
    //子弹能否穿透
    protected bool mIsPenetrate=false;
    protected int mPenetrateCount=0;
    protected virtual void Init(Character source,int points)
    {
        base.Init();
        mCollider=new MyCollider(GetComponent<PolygonCollider2D>());
        mSource = source;
        mPoints = points;
        mLiveTime=0;
        mMaxLifeTime=5;
        mPenetrateCount=99999;
        mIgnoreList=new List<Character>();
    }
    public override void OnUpdate()
    { 
        base.OnUpdate();
        mLiveTime+=Time.deltaTime;
        if(mLiveTime>=mMaxLifeTime)
        {
            mIsDead=true;
        }
    }

    //子弹碰撞角色造成伤害
    public virtual void OnCollideCharacter(Character target)
    {
        DamageInfo damageInfo=new DamageInfo(mSource,target,mPoints,this,null);
        if(mIsPenetrate&&mPenetrateCount!=0)
        {
            if(mIgnoreList.Contains(target))
            {
                return;
            }
            else
            {
                mIgnoreList.Add(target);
                FightSystem.Damage(damageInfo);
                target.PlayHurtSound();
                mPenetrateCount--;
            }
        }
        else
        {
            if(mIgnoreList.Contains(target))
            {
                return;
            }
            mIsDead=true;
            FightSystem.Damage(damageInfo);
            target.PlayHurtSound();
        }
    }

    public Character GetSource()
    {
        return mSource;
    }

}
