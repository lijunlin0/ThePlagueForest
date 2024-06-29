using System;
using UnityEngine;

//战斗事件数据

public class FightEventData
{
    protected FightEvent mFightEvent;
    protected Character mSource;
    protected Character mTarget;

    public Character GetSource(){return mSource;}
    public Character GetTarget(){return mTarget;}
    public FightEvent GetFightEvent(){return mFightEvent;}
}

public class FightEventDataDeath : FightEventData
{
    public FightEventDataDeath(Character character)
    {
        mFightEvent = FightEvent.Death;
        mSource = character;
        mTarget = character;
    }
}

public class FightEventDataDamage: FightEventData
{
    private DamageInfo mDamageInfo;
    public FightEventDataDamage(Character sourve,Character target,DamageInfo damageInfo)
    {
        mFightEvent = FightEvent.Damage;
        mSource = sourve;
        mTarget = target;
        mDamageInfo=damageInfo;
    }

    public DamageInfo GetDamageInfo(){return mDamageInfo;}
}

