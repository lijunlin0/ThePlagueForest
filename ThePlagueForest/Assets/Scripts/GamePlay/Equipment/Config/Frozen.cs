using System;
using System.Collections.Generic;

//极寒弹--10% *(layer) 子弹附带减速效果
public class Frozen : Equipment
{
    private const int FrozenBulletChance=10;
    public Frozen():base(EquipmentType.Passive,EquipmentId.Frozen)
    {
        mMaxlayer=4;
        mStatusEffectId=StatusEffectId.Equipment_Frozen;
    }

    public override void OnGet(StatusEffect effect, int layer)
    {
        effect.SetFightEventCallback((FightEventData eventData)=>
        {
            if(eventData.GetFightEvent()!=FightEvent.Damage)
            {
                return;
            }
            DamageInfo damageInfo=(eventData as FightEventDataDamage).GetDamageInfo();
            if(damageInfo.GetBullet()==null||damageInfo.GetTarget().IsPlayer())
            {
                return;
            }
            bool isFrozen = RandomHelper.PlayerRandomResultWithLucky(FrozenBulletChance*layer);;
            if(!isFrozen)
            {
                return;
            }
            Character target=eventData.GetTarget();
            StatusEffect frozenEffect=StatusEffectUtility.Frozen(target);
            StatusEffectChangeInfo changeInfo=new StatusEffectChangeInfo(frozenEffect,StatusEffectChangeReason.Fight,eventData.GetSource());
            FightSystem.AddStatusEffect(changeInfo);
                    
        });
    }
}