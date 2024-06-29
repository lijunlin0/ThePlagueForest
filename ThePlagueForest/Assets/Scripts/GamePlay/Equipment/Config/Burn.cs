using System;
using System.Collections.Generic;


//炽焰弹--10% 子弹附带燃烧效果
public class Burn : Equipment
{
    private const int BurnBulletChance=20;
    public Burn():base(EquipmentType.Passive,EquipmentId.Burn)
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
            bool isBurn = random.Next(100)<BurnBulletChance;
            if(!isBurn)
            {
                return;
            }
            Character target=eventData.GetTarget();
            
            StatusEffect burnEffect=StatusEffectUtility.Burn(target);
            StatusEffectChangeInfo changeInfo=new StatusEffectChangeInfo(burnEffect,StatusEffectChangeReason.Fight,eventData.GetSource());
            FightSystem.AddStatusEffect(changeInfo);
                    
        });
    }
}