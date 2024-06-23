using System.Collections.Generic;

//短剑--增加 10% 攻击力 
public class Sword : Equipment
{
    private const int mDamageAddition=10;
    public Sword():base(EquipmentType.Passive,EquipmentId.Sword)
    {

    }
    public override void OnGet(StatusEffect effect,int layer)
    {
        Dictionary<Property,float> corrections=new Dictionary<Property,float>();
        corrections.Add(Property.DamageAddition,mDamageAddition*layer);
        effect.SetPropertyCorrections(corrections);
    }
}