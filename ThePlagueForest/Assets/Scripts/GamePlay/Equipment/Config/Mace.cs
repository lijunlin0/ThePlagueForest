using System.Collections.Generic;

//长矛--增加范围攻击 10% 的攻击范围 
public class Mace : Equipment
{
    private const int mAoeRangeAddition=20;
    public Mace():base(EquipmentType.Passive,EquipmentId.Mace)
    {
        mMaxlayer=5;
        mStatusEffectId=StatusEffectId.Equipment_Mace;
    }
    public override void OnGet(StatusEffect effect,int layer)
    {
        Dictionary<Property,float> corrections=new Dictionary<Property,float>();
        corrections.Add(Property.AoeRange,mAoeRangeAddition*layer);
        effect.SetPropertyCorrections(corrections);
    }
}