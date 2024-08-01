using System.Collections.Generic;

//王冠--每 N 秒无敌 Y 秒
public class Crown : Equipment
{
    private const int mInvincibleTick=8;
    private const int mInvincibleDuration=2;
    private const int TickLayerReduce=1;
    public Crown():base(EquipmentType.Passive,EquipmentId.Crown)
    {
        mMaxlayer=3;
    }
    public override void OnGet(StatusEffect effect,int layer)
    {
        effect.SetTick(mInvincibleTick-(TickLayerReduce*layer-1),()=>
        {
            StatusEffect statusEffect=new StatusEffect(StatusEffectId.Invincible,Player.GetCurrent(),mInvincibleDuration);
            StatusEffectChangeInfo changeInfo=new StatusEffectChangeInfo(statusEffect,StatusEffectChangeReason.Fight,Player.GetCurrent());
            FightSystem.AddStatusEffect(changeInfo);
        });
        
    }
}