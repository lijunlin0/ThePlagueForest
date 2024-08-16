//活力戒指--击杀敌人有 25% 概率恢复 1+(Layer*1)% 最大生命值 的血量
public class Vitality : Equipment
{
    private const float mTriggerChance=25;
    private const int HealthPercent=1;
    private const int HealthPercentLayerAddition=1;
    public Vitality():base(EquipmentType.Passive,EquipmentId.Vitality)
    {
        mMaxlayer=3;
        mStatusEffectId=StatusEffectId.Equipment_Vitality;
    }
    public override void OnGet(StatusEffect effect,int layer)
    {
        effect.SetFightEventCallback((FightEventData eventData)=>
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
            int points=player.GetCurrentPropertySheet().GetMaxHealth()*(HealthPercent+HealthPercentLayerAddition*(layer-1))/100;
            RecoveryInfo recoveryInfo=new RecoveryInfo(player,player,points);
            FightSystem.Recovery(recoveryInfo);
        });
    }

    public void OnFightEvent(FightEventData eventData)
    {
       
        
        
    }
}