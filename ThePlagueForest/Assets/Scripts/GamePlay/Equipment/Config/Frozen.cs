using System;
using System.Collections.Generic;

//炽焰弹--10% 子弹附带燃烧效果
public class Frozen : Equipment
{
    private const int FrozenBulletChance=20;
    public Frozen():base(EquipmentType.Passive,EquipmentId.Frozen)
    {

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
            if(damageInfo.GetBullet()==null&&damageInfo.GetSource().IsPlayer())
            {
                return;
            }
            Random random=new Random();
            bool isFrozen = random.Next(100)<FrozenBulletChance;
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