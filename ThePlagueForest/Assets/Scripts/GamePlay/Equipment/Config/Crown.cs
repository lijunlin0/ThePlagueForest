using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

//王冠--每 N 秒无敌 Y 秒
public class Crown : Equipment
{
    private const int mInvincibleTick=8;
    private const int mInvincibleDuration=2;
    private const int TickLayerReduce=1;
    public Crown():base(EquipmentType.Passive,EquipmentId.Crown)
    {
        mMaxlayer=3;
        mStatusEffectId=StatusEffectId.Equipment_Crown;
    }
    public override void OnGet(StatusEffect effect,int layer)
    {     
        CrownObject crownObject=Player.GetCurrent().GetCrownObject();
        int tick=mInvincibleTick-TickLayerReduce*(layer-1);
        effect.SetTick(tick,()=>
        {
            crownObject.gameObject.SetActive(true);
            DOVirtual.DelayedCall(mInvincibleDuration,()=>
            {
               crownObject.gameObject.SetActive(false);
            });

            StatusEffect statusEffect=new StatusEffect(StatusEffectId.Invincible,Player.GetCurrent(),mInvincibleDuration);
            StatusEffectChangeInfo changeInfo=new StatusEffectChangeInfo(statusEffect,StatusEffectChangeReason.Fight,Player.GetCurrent());
            FightSystem.AddStatusEffect(changeInfo);
        });
        
    }
}