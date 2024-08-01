using System.Collections.Generic;

//全能护身符--增加 3% 攻击力,3% 攻击速度, 3% 生命值, 3% 移动速度
public class Amulet  : Equipment
{
    private const int mDamageAddition=3;
    private const int mAttackSpeedAddition=3;
    private const int mMaxHealthAddition=3;
    private const int mMoveSpeedAddition=3;

    public Amulet():base(EquipmentType.Passive,EquipmentId.Amulet)
    {
    }

    public override void OnGet(StatusEffect statusEffect, int layer)
    {
        Dictionary<Property,float> corrections=new Dictionary<Property,float>();
        corrections.Add(Property.DamageAddition,mDamageAddition*layer);
        corrections.Add(Property.AttackSpeedAddition,mAttackSpeedAddition*layer);
        corrections.Add(Property.HealthAddition,mMaxHealthAddition*layer);
        corrections.Add(Property.MoveSpeedAddition,mMoveSpeedAddition*layer);
        statusEffect.SetPropertyCorrections(corrections);
    }
}