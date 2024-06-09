using System.Collections.Generic;

public class Sword : Equipment
{
    private const int AttackAddition=10;
    public Sword():base(EquipmentType.Passive,EquipmentId.Sword)
    {

    }
    public override void OnGet(StatusEffect effect,int layer)
    {
        Dictionary<Property,double> corrections=new Dictionary<Property,double>();
        corrections.Add(Property.AttackAddition,AttackAddition*layer);
        effect.SetPropertyCorrections(corrections);
    }
}