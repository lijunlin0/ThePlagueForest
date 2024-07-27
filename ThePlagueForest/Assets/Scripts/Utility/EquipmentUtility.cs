using System.Collections.Generic;

public class EquipmentUtility
{
    private static Dictionary<EquipmentId,Equipment> sEquipments;
    public static void Init()
    {
        sEquipments=new Dictionary<EquipmentId,Equipment>();
        sEquipments.Add(EquipmentId.Sword,new Sword());
        sEquipments.Add(EquipmentId.Karambit,new Karambit());
        sEquipments.Add(EquipmentId.Shoe,new Shoe());
        sEquipments.Add(EquipmentId.Armour,new Armour());
        sEquipments.Add(EquipmentId.Sickle,new Sickle());
        sEquipments.Add(EquipmentId.Amulet,new Amulet());
        sEquipments.Add(EquipmentId.Vitality,new Vitality());
        sEquipments.Add(EquipmentId.Spear,new Spear());
        sEquipments.Add(EquipmentId.Heartstone,new Heartstone());
        sEquipments.Add(EquipmentId.Burn,new Burn());
        sEquipments.Add(EquipmentId.Frozen,new Frozen());
        sEquipments.Add(EquipmentId.LifeEmblem,new LifeEmblem());
        sEquipments.Add(EquipmentId.Crown,new Crown());
        sEquipments.Add(EquipmentId.BurnCircle,new BurnCircle());
        sEquipments.Add(EquipmentId.FrozenCircle,new FrozenCircle());
        sEquipments.Add(EquipmentId.LuckyStone,new LuckyStone());
        sEquipments.Add(EquipmentId.Boomerang,new Boomerang());
        sEquipments.Add(EquipmentId.Dagger,new Dagger());
        sEquipments.Add(EquipmentId.DeathBomb,new DeathBomb());
        sEquipments.Add(EquipmentId.FireWand,new FireWand());
        sEquipments.Add(EquipmentId.SacredSword,new SacredSword());
        sEquipments.Add(EquipmentId.ThunderWand,new ThunderWand());
        sEquipments.Add(EquipmentId.StunGun,new StunGun());
 
    }
    public static Equipment GetEquipment(EquipmentId equipmentId)
    {
        return sEquipments[equipmentId];
    }
}