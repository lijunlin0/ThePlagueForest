using System.Collections.Generic;

//镰刀--子弹命中敌人时 30% 概率恢复 2 生命值
public class Sickle : Equipment
{
    private const float mTriggerChance=30; 
    private const int mRecoveryPoints=2;
    public Sickle():base(EquipmentType.Passive,EquipmentId.Sickle)
    {

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
            RecoveryInfo recoveryInfo=new RecoveryInfo(player,player,mRecoveryPoints);
            FightSystem.Recovery(recoveryInfo);        
        });
    }
}