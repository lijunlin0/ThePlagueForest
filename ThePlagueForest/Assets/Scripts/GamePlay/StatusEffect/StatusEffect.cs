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
    private Dictionary<Property,double> mPropertyCorrections;
    private bool mIsDead;

    private Character mTarget;
    private StatusEffectId mId;

    public StatusEffect(StatusEffectId id,Character target)
    {
        mId = id;
        mTarget = target;
    }
    
    public bool IsDead(){return mIsDead;}
    public Character GetTarget(){return mTarget;}
    public Dictionary<Property,double> GetPropertyCorrections() { return mPropertyCorrections;}
    public void SetPropertyCorrections(Dictionary<Property,double> corrections) {mPropertyCorrections=corrections;}
    public StatusEffectId GetId(){return mId;}
    
}