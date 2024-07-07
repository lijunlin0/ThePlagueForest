using System.Collections.Generic;

//王冠--每 8 秒无敌 2 秒
public class Crown : Equipment
{
    private const int mInvincibleTick=4;
    private const int mInvincibleDuration=2;
    public Crown():base(EquipmentType.Passive,EquipmentId.Crown)
    {

    }
    public override void OnGet(StatusEffect effect,int layer)
    {
        effect.SetTick(mInvincibleTick,()=>
        {
            StatusEffect statusEffect=new StatusEffect(StatusEffectId.Invincible,Player.GetCurrent(),mInvincibleDuration);
            StatusEffectChangeInfo changeInfo=new StatusEffectChangeInfo(statusEffect,StatusEffectChangeReason.Fight,Player.GetCurrent());
            FightSystem.AddStatusEffect(changeInfo);
        });
        
    }
}