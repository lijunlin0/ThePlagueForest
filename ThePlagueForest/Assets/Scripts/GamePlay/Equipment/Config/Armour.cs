using System.Collections.Generic;

//盔甲--最大生命值增加 100
public class Armour : Equipment
{
    private const int mMaxHealthAddition=100;
    public Armour():base(EquipmentType.Passive,EquipmentId.Armour)
    {
        mStatusEffectId=StatusEffectId.Equipment_Armour;
    }

    public override void OnGet(StatusEffect statusEffect, int layer)
    {
        Dictionary<Property,float> corrections=new Dictionary<Property,float>();
        corrections.Add(Property.HealthAdditionPoint,mMaxHealthAddition*layer);
        statusEffect.SetPropertyCorrections(corrections);
    }
}