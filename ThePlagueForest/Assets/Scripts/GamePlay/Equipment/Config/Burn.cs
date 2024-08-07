using System;
using System.Collections.Generic;


//炙热弹--10% *(layer)子弹附带燃烧效果
public class Burn : Equipment
{
    private const int BurnBulletChance=10;
    public Burn():base(EquipmentType.Passive,EquipmentId.Burn)
    {
        mMaxlayer=4;
        mStatusEffectId=StatusEffectId.Equipment_Burn;
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
            bool isBurn = RandomHelper.PlayerRandomResultWithLucky(BurnBulletChance*layer);
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