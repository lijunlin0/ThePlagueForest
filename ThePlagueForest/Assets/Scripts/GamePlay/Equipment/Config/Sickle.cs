using System.Collections.Generic;

//镰刀--子弹命中敌人时 30% 概率恢复 2+((layer-1)*1) 生命值
public class Sickle : Equipment
{
    private const float mTriggerChance=30; 
    private const int mRecoveryPoints=2;
    private const int mRecoveryPointsLayerAddition=1;
    public Sickle():base(EquipmentType.Passive,EquipmentId.Sickle)
    {
        mMaxlayer=3;
        mStatusEffectId=StatusEffectId.Equipment_Sickle;
    }

    public override void OnGet(StatusEffect statusEffect, int layer)
    {
        statusEffect.SetFightEventCallback((FightEventData eventData)=>
        {
            if(eventData.GetFightEvent()!=FightEvent.Damage)
            {
                return;
            }
            
            if(eventData.GetTarget().IsPlayer())
            {
                return;
            }
            DamageInfo damageInfo=(eventData as FightEventDataDamage).GetDamageInfo();
            if(damageInfo.GetBullet()==null)
            {
                return;
            }

            Player player=Player.GetCurrent();
            float chance=player.GetCurrentPropertySheet().GetLuckyFactor()*mTriggerChance;
            bool IsTrigger=RandomHelper.RandomResult(chance);
            if(!IsTrigger)
            {
                return;
            }
            int points=mRecoveryPoints+mRecoveryPointsLayerAddition*(layer-1);
            RecoveryInfo recoveryInfo=new RecoveryInfo(player,player,points);
            FightSystem.Recovery(recoveryInfo);        
        });
    }
}