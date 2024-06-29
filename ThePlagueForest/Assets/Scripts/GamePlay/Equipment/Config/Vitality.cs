using System.Collections.Generic;

//活力戒指--击杀敌人恢复 1% 最大生命值
public class Vitality : Equipment
{
    private const int HealthPercent=1;
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
        if(eventData.GetSource().IsPlayer())
        {
            return;
        }
        Player player=Player.GetCurrent();
        int points=player.GetCurrentPropertySheet().GetMaxHealth()*HealthPercent/100;
        RecoveryInfo recoveryInfo=new RecoveryInfo(player,player,points);
        FightSystem.Recovery(recoveryInfo);
        
    }
}