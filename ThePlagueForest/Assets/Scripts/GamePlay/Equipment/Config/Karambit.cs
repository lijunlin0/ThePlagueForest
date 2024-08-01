using System.Collections.Generic;

//鹰爪--增加 10% 攻击速度
public class Karambit : Equipment
{
    private const int mAttackSpeedAddition=10;
    public Karambit():base(EquipmentType.Passive,EquipmentId.Karambit)
    {
    }

    public override void OnGet(StatusEffect statusEffect, int layer)
    {
        Dictionary<Property,float> corrections=new Dictionary<Property,float>();
        corrections.Add(Property.AttackSpeedAddition,mAttackSpeedAddition*layer);
        statusEffect.SetPropertyCorrections(corrections);
    }
}