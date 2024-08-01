using System.Collections.Generic;

//冰圈--持续减速周围敌人
public class FrozenCircle : Equipment
{
    private const float mFrozenTick=0.1f;
    public FrozenCircle():base(EquipmentType.Passive,EquipmentId.FrozenCircle)
    {
        mMaxlayer=1;
    }

    public override void OnGet(StatusEffect statusEffect, int layer)
    {
        EffectArea area=EffectArea.Create("FrozenCircle",(Character target)=>
        {
            StatusEffect effect=StatusEffectUtility.Frozen(target);
            StatusEffectChangeInfo changeInfo=new StatusEffectChangeInfo(effect,StatusEffectChangeReason.Fight,Player.GetCurrent());
            FightSystem.AddStatusEffect(changeInfo);
        });
        statusEffect.SetTick(mFrozenTick,()=>
        {
            area.Collide();
        });
    }
}