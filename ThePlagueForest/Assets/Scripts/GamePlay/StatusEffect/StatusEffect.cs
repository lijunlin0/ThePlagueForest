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
    private Dictionary<Property,float> mPropertyCorrections;
    private bool mIsDead;

    private Character mTarget;
    private StatusEffectId mId;
    private Callback<FightEventData> mFightEventCallback;
    public StatusEffect(StatusEffectId id,Character target)
    {
        mId = id;
        mTarget = target;
    }
    
    public bool IsDead(){return mIsDead;}
    public Character GetTarget(){return mTarget;}
    public Dictionary<Property,float> GetPropertyCorrections() { return mPropertyCorrections;}
    public void SetPropertyCorrections(Dictionary<Property,float> corrections) {mPropertyCorrections=corrections;}
    public StatusEffectId GetId(){return mId;}

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