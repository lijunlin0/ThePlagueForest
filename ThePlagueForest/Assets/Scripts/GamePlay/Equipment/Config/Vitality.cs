using System.Collections.Generic;

//活力戒指--击杀敌人有 25% 概率恢复 4% 最大生命值
public class Vitality : Equipment
{
    private const float mTriggerChance=25;
    private const int HealthPercent=4;
    public Vitality():base(EquipmentType.Passive,EquipmentId.Vitality)
    {

    }
    public override void OnGet(StatusEffect effect,int layer)
    {
        effect.SetFightEventCallback(OnFightEvent);
    }

    public void OnFightEvent(FightEventData eventData)
    {
       
        if(eventData.GetFightEvent()!=FightEvent.Death)
        {
            return;
        }
        if(eventData.GetTarget().IsPlayer())
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
        int points=player.GetCurrentPropertySheet().GetMaxHealth()*HealthPercent/100;
        RecoveryInfo recoveryInfo=new RecoveryInfo(player,player,points);
        FightSystem.Recovery(recoveryInfo);
        
    }
}