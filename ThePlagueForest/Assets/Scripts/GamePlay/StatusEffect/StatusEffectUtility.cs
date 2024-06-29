using System;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;

public class StatusEffectUtility
{
    //燃烧: 每 0.5 秒造成 4% 最大生命值的伤害（对 Boss 为 1%）, 持续 4 秒
    public static StatusEffect Burn(Character target)
    {
        float percent=StatusEffect.BurnTickDamageWithMaxHealth;
        if(target.IsBoss())
        {
            percent=StatusEffect.BurnTickDamageWithMaxHealthBoss;
        }
        StatusEffect statusEffect=new StatusEffect(StatusEffectId.Burn,target,StatusEffect.BurnEffectDuration);
        statusEffect.SetTick(StatusEffect.BurnTick,()=>
        {
            int points=(int)(target.GetCurrentPropertySheet().GetMaxHealth()*percent);
            DamageInfo damageInfo=new DamageInfo(target,target,points,null,statusEffect);
            FightSystem.Damage(damageInfo);
        });
        return statusEffect;
    }
    //冰冻: 移动速度 -40%（对 Boss 为 -10%），持续 3 秒
    public static StatusEffect Frozen(Character target)
    {
        StatusEffect statusEffect=new StatusEffect(StatusEffectId.Frozen,target,StatusEffect.FrozenEffectDuration);
        Dictionary<Property,float> corrections=new Dictionary<Property,float>();
        corrections.Add(Property.MoveSpeedAddition,StatusEffect.FrozenMoveSpeedFactor);
        statusEffect.SetPropertyCorrections(corrections);
        return statusEffect;
    }
}