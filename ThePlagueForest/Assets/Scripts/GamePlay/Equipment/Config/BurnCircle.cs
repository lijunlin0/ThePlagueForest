using System.Collections.Generic;

//火圈--持续燃烧周围敌人
public class BurnCircle : Equipment
{
    private const float mBurnTick=0.1f;
    public BurnCircle():base(EquipmentType.Passive,EquipmentId.BurnCircle)
    {

    }

    public override void OnGet(StatusEffect statusEffect, int layer)
    {
        EffectArea area=EffectArea.Create("BurnCircle",(Character target)=>
        {
            StatusEffect effect=StatusEffectUtility.Burn(target);
            StatusEffectChangeInfo changeInfo=new StatusEffectChangeInfo(effect,StatusEffectChangeReason.Fight,Player.GetCurrent());
            FightSystem.AddStatusEffect(changeInfo);
        });
        statusEffect.SetTick(mBurnTick,()=>
        {
            area.Collide();
        });
    }
}