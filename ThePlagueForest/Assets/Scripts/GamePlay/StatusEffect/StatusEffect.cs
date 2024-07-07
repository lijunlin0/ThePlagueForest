using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectRemoveReason
{
    Expire,     //持续时间到期
    Force,      //强制移除
}

public class StatusEffect
{
    //无限叠加的状态效果最大层数
    public const int InfiniteLayer = 1000;
    public const float InfiniteDuration=999999;
    private Dictionary<Property,float> mPropertyCorrections;
    private bool mIsDead;

    private Character mTarget;
    private StatusEffectId mId;
    private int mMaxLayer;//todo
    private Dictionary<string,string> mUserData;
    private Callback<FightEventData> mFightEventCallback;

    //燃烧,冰冻默认持续时间
    public const float BurnDuration = 4f;
    public const float FrozenDuration = 4f;

    //燃烧每次伤害最大生命值比例
    public const float BurnTickDamageWithMaxHealth=4;
    public const float BurnTickDamageWithMaxHealthBoss=1;

    //冰冻减速比例
    public const float FrozenMoveSpeedAddition=-90;


    public const float BurnTick = 0.5f;

    //持续时间计时
    private float mTotalDuration;
    private float mElapseDuration;

    //Tick计时
    private float mTotalTickDuration;
    private float mElapsedTickDuration;
    private Callback mTickCallback;
    
    public StatusEffect(StatusEffectId id,Character target,float totalDuration=InfiniteDuration)
    {
        mId = id;
        mTarget = target;
        mPropertyCorrections=new Dictionary<Property,float>();

        mTotalDuration=totalDuration;
        mElapsedTickDuration=0;
        mTotalTickDuration=999999;
        mIsDead=false;
        mMaxLayer=InfiniteLayer;
    }
    public int GetMaxLayer(){return mMaxLayer;}
    public void SetMaxLayer(int layer){mMaxLayer=layer;}
    public bool IsDead(){return mIsDead;}
    public Character GetTarget(){return mTarget;}
    public Dictionary<Property,float> GetPropertyCorrections() { return mPropertyCorrections;}
    public void SetPropertyCorrections(Dictionary<Property,float> corrections) {mPropertyCorrections=corrections;}
    public StatusEffectId GetId(){return mId;}

    public void OnUpdate()
    {
        if(mIsDead)
        {
            return;
        }
        //是否需要进行Tick
        if(mTickCallback!=null)
        {
            mElapsedTickDuration+=Time.deltaTime;
            if(mElapsedTickDuration>mTotalTickDuration)
            {
                mElapsedTickDuration-=mTotalTickDuration;
                mTickCallback();
            }
        }

        //状态效果持续时间是否结束
        mElapseDuration+=Time.deltaTime;
        if(mElapseDuration>mTotalDuration)
        {
            mIsDead=true;
        }

    }

    public float GetElapsedTickDuration(){return mElapsedTickDuration;}
    public void  SetElapsedTickDuration(float duration){mElapsedTickDuration=duration;}
    public void SetTick(float tickDuration, Callback tickCallback)
    {
        mTotalTickDuration=tickDuration;
        Debug.Log("mTotalTickDuration:"+mTotalTickDuration);
        mTickCallback=tickCallback;
    }
    public string GetUserData(string key)
    {
        if(mUserData==null||!mUserData.ContainsKey(key))
        {
            return string.Empty;
        }
        return mUserData[key];
    }
    public void SetUserData(string key,string value)
    {
        if(mUserData==null)
        {
            mUserData=new Dictionary<string, string>();
        }
        mUserData[key]=value;
    }

    public void SetFightEventCallback(Callback<FightEventData> callback)
    {
        mFightEventCallback = callback;
    }

    public void OnFightEvent(FightEventData eventData)
    {
        if(mFightEventCallback!=null)
        {
            mFightEventCallback(eventData);
        }
    }
    
}