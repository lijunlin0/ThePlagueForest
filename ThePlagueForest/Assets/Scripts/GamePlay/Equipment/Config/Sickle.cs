using System.Collections.Generic;

//镰刀--增加 0.5% 生命偷取率
public class Sickle : Equipment
{
    private float mLifeSteal=0.5f;
    public Sickle():base(EquipmentType.Passive,EquipmentId.Sickle)
    {

    }

    public override void OnGet(StatusEffect statusEffect, int layer)
    {
        Dictionary<Property,float> corrections=new Dictionary<Property,float>();
        corrections.Add(Property.LifeSteal,mLifeSteal*layer);
        statusEffect.SetPropertyCorrections(corrections);        
    }
}