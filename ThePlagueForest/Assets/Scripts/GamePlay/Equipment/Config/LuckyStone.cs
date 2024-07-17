using System.Collections.Generic;

//幸运石--增加 30 的幸运值
public class LuckyStone : Equipment
{
    private const int mLuckyPercent=30;
    public LuckyStone():base(EquipmentType.Passive,EquipmentId.LuckyStone)
    {

    }

    public override void OnGet(StatusEffect statusEffect, int layer)
    {
        Dictionary<Property,float> corrections=new Dictionary<Property,float>();
        corrections.Add(Property.Lucky,mLuckyPercent*layer);
        statusEffect.SetPropertyCorrections(corrections);        
    }
}