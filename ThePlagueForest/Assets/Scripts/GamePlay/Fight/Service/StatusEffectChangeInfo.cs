using System;

public enum StatusEffectChangeReason
{
    System,     //系统触发的, 必然添加
    Fight,      //战斗触发的, 可以被阻挡
}

public class StatusEffectChangeInfo
{
    private StatusEffect mStatusEffect;
    private StatusEffectChangeReason mChangeReason;
    private Character mSource;

    public StatusEffectChangeInfo(StatusEffect statusEffect,StatusEffectChangeReason changeReason, Character source)
    {
        mStatusEffect=statusEffect;
        mChangeReason=changeReason;
        mSource=source;
    }

    public StatusEffect GetStatusEffect(){return mStatusEffect;}
    public StatusEffectChangeReason GetChangeReason(){return mChangeReason;}
    public Character GetSource(){return mSource;} 
}