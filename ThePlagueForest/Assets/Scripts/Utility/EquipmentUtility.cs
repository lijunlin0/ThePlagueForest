using System;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUtility
{
    private const int EquipmentSelectCount=3;
    private static JsonData Config;
    private static Dictionary<EquipmentId,Equipment> sEquipments;
    public static void Init()
    {
        TextAsset configText=Resources.Load<TextAsset>("Config/GameTextConfig");
        Config=JsonMapper.ToObject(configText.text);
        sEquipments=new Dictionary<EquipmentId,Equipment>();
        sEquipments.Add(EquipmentId.Crown,new Crown());
        sEquipments.Add(EquipmentId.ThunderWand,new ThunderWand());
        sEquipments.Add(EquipmentId.FireWand,new FireWand());
        sEquipments.Add(EquipmentId.Boomerang,new Boomerang());
        sEquipments.Add(EquipmentId.Dagger,new Dagger());
       

        return;
        sEquipments.Add(EquipmentId.Sword,new Sword());
        sEquipments.Add(EquipmentId.Karambit,new Karambit());
        sEquipments.Add(EquipmentId.Shoe,new Shoe());
        sEquipments.Add(EquipmentId.Armour,new Armour());
        sEquipments.Add(EquipmentId.Sickle,new Sickle());
        sEquipments.Add(EquipmentId.Amulet,new Amulet());
        sEquipments.Add(EquipmentId.Vitality,new Vitality());
        sEquipments.Add(EquipmentId.Mace,new Mace());
        sEquipments.Add(EquipmentId.Heartstone,new Heartstone());
        sEquipments.Add(EquipmentId.Burn,new Burn());
        sEquipments.Add(EquipmentId.Frozen,new Frozen());
        sEquipments.Add(EquipmentId.LifeEmblem,new LifeEmblem());
        sEquipments.Add(EquipmentId.BurnCircle,new BurnCircle());
        sEquipments.Add(EquipmentId.FrozenCircle,new FrozenCircle());
        sEquipments.Add(EquipmentId.LuckyStone,new LuckyStone());

         sEquipments.Add(EquipmentId.DeathBomb,new DeathBomb());

        sEquipments.Add(EquipmentId.SacredSword,new SacredSword());
        sEquipments.Add(EquipmentId.StunGun,new StunGun());
    }
    public static Equipment GetEquipment(EquipmentId equipmentId)
    {
        return sEquipments[equipmentId];
    }
    public static Tuple<string,string,string> GetEquipmentText(EquipmentId id)
    {
        JsonData EquipmentData=Config[id.ToString()];
        var tuple=Tuple.Create(EquipmentData[0].ToString(),EquipmentData[1].ToString(),EquipmentData[2].ToString());
        return tuple;
    }

    public static List<Equipment> GetAvailableEquipments()
    {
        FightModel fightModel=FightModel.GetCurrent();
        List<Equipment> equipments=sEquipments.Values.ToList();
        List<Equipment> res=new List<Equipment>();
        foreach(Equipment equipment in equipments)
        {
            if(fightModel.GetEquipmentLayer(equipment)>equipment.GetMaxLayer())
            {
                continue;
            }       
            res.Add(equipment);
        }
        equipments.Clear();
        
        for(int i=0;i<EquipmentSelectCount;i++)
        {
            int randnumIndex=RandomHelper.RandomInt(0,res.Count-1);
            equipments.Add(res[randnumIndex]);
            res.RemoveAt(randnumIndex);
        }
        return equipments;
    }
}